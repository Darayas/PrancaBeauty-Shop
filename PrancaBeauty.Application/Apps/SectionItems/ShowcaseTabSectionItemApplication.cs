using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionItems.Contracts;
using PrancaBeauty.Domin.Showcases.SectionItems.Entitiy;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.SectionItems
{
    public class ShowcaseTabSectionItemApplication : IShowcaseTabSectionItemApplication
    {
        private readonly ILogger _Logger;
        public readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IShowcaseTabSectionItemRepository _ShowcaseTabSectionItemRepository;
        private readonly IShowcaseTabSectionFreeItemTranslateRepository _ShowcaseTabSectionFreeItemTranslateRepository;

        public ShowcaseTabSectionItemApplication(ILogger Logger, IServiceProvider ServiceProvider, ILocalizer Localizer, IShowcaseTabSectionItemRepository ShowcaseTabSectionItemRepository, IShowcaseTabSectionFreeItemTranslateRepository showcaseTabSectionFreeItemTranslateRepository)
        {
            _Logger= Logger;
            _ServiceProvider= ServiceProvider;
            _Localizer= Localizer;
            _ShowcaseTabSectionItemRepository= ShowcaseTabSectionItemRepository;
            _ShowcaseTabSectionFreeItemTranslateRepository=showcaseTabSectionFreeItemTranslateRepository;
        }

        public async Task<(OutPagingData, List<OutGetListShowcaseTabSectionItemForAdminPage>)> GetListShowcaseTabSectionItemForAdminPageAsync(InpGetListShowcaseTabSectionItemForAdminPage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _ShowcaseTabSectionItemRepository.Get
                                                .Where(a => a.TabSectionId==Input.ShowcaseTabSectionId.ToGuid())
                                                .Select(a => new OutGetListShowcaseTabSectionItemForAdminPage
                                                {
                                                    Id=a.Id.ToString(),
                                                    Sort=a.Sort,
                                                    SectionType=_Localizer[a.SectionType.ToString()],
                                                    Title= a.SectionType==tblShowcaseTabSectionItemsEnum.FreeItem ?
                                                                                        a.tblSectionFreeItems.tblSectionFreeItemTranslate.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single()
                                                                : (a.SectionType==tblShowcaseTabSectionItemsEnum.Product ?
                                                                                        a.tblSectionProducts.tblProducts.Title
                                                                       : (a.SectionType==tblShowcaseTabSectionItemsEnum.Category ?
                                                                                        a.tblSectionProductCategory.tblCategory.tblCategory_Translates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single()
                                                                                : (a.SectionType== tblShowcaseTabSectionItemsEnum.Keyword ?
                                                                                        a.tblSectionProductKeyword.tblKeywords.Title : "")))

                                                }).OrderBy(a => a.Sort);

                var _PagingData = PagingData.Calc(await qData.LongCountAsync(), Input.Page, Input.Take);
                return (_PagingData, await qData.Skip((int)_PagingData.Skip).Take(_PagingData.Take).ToListAsync());

            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }

        public async Task<OperationResult> AddTabSectionFreeItemAsync(InpAddTabSectionFreeItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Check duplicate name
                if (await _ShowcaseTabSectionItemRepository.Get.Where(a => a.tblSectionFreeItems.tblShowcaseTabSectionItems.TabSectionId==Input.ShowcaseTabSectionId.ToGuid()).AnyAsync(a => a.tblSectionFreeItems.Name==Input.Name))
                    return new OperationResult().Failed("NameIsDuplicated");

                #endregion

                #region Check duplicate title
                var HasDuplicateTitle = (await _ShowcaseTabSectionFreeItemTranslateRepository.Get
                                                            .Where(a => a.tblSectionFreeItems.tblShowcaseTabSectionItems.TabSectionId==Input.ShowcaseTabSectionId.ToGuid())
                                                            .Select(a => a.Title)
                                                            .ToListAsync())
                                                                    .Where(Title => Input.LstTranslate.Where(b => b.Title==Title).Any())
                                                                    .Any();
                if (HasDuplicateTitle)
                    return new OperationResult().Failed("TitleLangIsDuplicate");
                #endregion

                #region Get sort num
                int _Sort = 0;
                {
                    _Sort= await _ShowcaseTabSectionItemRepository.Get.Where(a => a.TabSectionId==Input.ShowcaseTabSectionId.ToGuid()).CountAsync();
                }
                #endregion

                #region Add Item
                {
                    var tTabSectionFreeItem = new tblShowcaseTabSectionItems
                    {
                        Id=new Guid().SequentialGuid(),
                        TabSectionId=Input.ShowcaseTabSectionId.ToGuid(),
                        SectionType=tblShowcaseTabSectionItemsEnum.FreeItem,
                        Sort=_Sort,
                        tblSectionFreeItems= new tblSectionFreeItems
                        {
                            Id=new Guid().SequentialGuid(),
                            Name=Input.Name,
                            tblSectionFreeItemTranslate= Input.LstTranslate.Select(a => new tblSectionFreeItemTranslate
                            {
                                Id=new Guid().SequentialGuid(),
                                LangId=a.LangId.ToGuid(),
                                FileId=a.FileId.ToGuid(),
                                Title=a.Title,
                                Url=a.Url,
                                HtmlText=a.HtmlText.GetSanitizeHtml(),
                            }).ToList()
                        }
                    };

                    await _ShowcaseTabSectionItemRepository.AddAsync(tTabSectionFreeItem, default, true);
                }
                #endregion

                return new OperationResult().Succeeded();

            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
