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
using PrancaBeauty.Domin.Showcases.SectionProductAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                                                    SectionType=a.SectionType.ToString(),
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

                #region Check SectionType
                {
                    var _Result = await CheckTabSectionTypeAsync(Input.ShowcaseTabSectionId, tblShowcaseTabSectionItemsEnum.FreeItem);
                    if (_Result.IsSucceeded==false)
                        return new OperationResult().Failed(_Result.Message);
                }
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

        public async Task<OperationResult> AddTabSectionProductItemAsync(InpAddTabSectionProductItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Check SectionType
                {
                    var _Result = await CheckTabSectionTypeAsync(Input.ShowcaseTabSectionId, tblShowcaseTabSectionItemsEnum.Product);
                    if (_Result.IsSucceeded==false)
                        return new OperationResult().Failed(_Result.Message);
                }
                #endregion

                #region Check duplicate productId
                if (await _ShowcaseTabSectionItemRepository.Get.Where(a => a.tblSectionFreeItems.tblShowcaseTabSectionItems.TabSectionId==Input.ShowcaseTabSectionId.ToGuid()).AnyAsync(a => a.tblSectionProducts.ProductId==Input.ProductId.ToGuid()))
                    return new OperationResult().Failed("ProductIsDuplicated");

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
                        SectionType=tblShowcaseTabSectionItemsEnum.Product,
                        Sort=_Sort,
                        tblSectionProducts= new tblSectionProducts
                        {
                            Id=new Guid().SequentialGuid(),
                            ProductId=Input.ProductId.ToGuid()
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

        public async Task<OperationResult> AddTabSectionCategoryItemAsync(InpAddTabSectionCategoryItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Check SectionType
                {
                    var _Result = await CheckTabSectionTypeAsync(Input.ShowcaseTabSectionId, tblShowcaseTabSectionItemsEnum.Category);
                    if (_Result.IsSucceeded==false)
                        return new OperationResult().Failed(_Result.Message);
                }
                #endregion

                #region Check Has Category
                if (await _ShowcaseTabSectionItemRepository.Get.Where(a => a.TabSectionId==Input.ShowcaseTabSectionId.ToGuid()).AnyAsync(a => a.SectionType==tblShowcaseTabSectionItemsEnum.Category))
                    return new OperationResult().Failed("InCategorySectionType,YouAreAllowedAddOneItem");

                #endregion

                #region Get sort num
                int _Sort = 0;
                {
                    _Sort= await _ShowcaseTabSectionItemRepository.Get.Where(a => a.TabSectionId==Input.ShowcaseTabSectionId.ToGuid()).CountAsync();
                }
                #endregion

                #region Add Item
                {
                    var tTabSectionItem = new tblShowcaseTabSectionItems
                    {
                        Id=new Guid().SequentialGuid(),
                        TabSectionId=Input.ShowcaseTabSectionId.ToGuid(),
                        SectionType=tblShowcaseTabSectionItemsEnum.Category,
                        Sort=_Sort,
                        tblSectionProductCategory= new tblSectionProductCategory
                        {
                            Id=new Guid().SequentialGuid(),
                            CategoryId=Input.CategoryId.ToGuid(),
                            CountFetch=Input.CountFetch,
                            OrderBy=(tblSectionProductCategoryOrderByEnum)Input.OrderBy
                        }
                    };

                    await _ShowcaseTabSectionItemRepository.AddAsync(tTabSectionItem, default, true);
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

        public async Task<OperationResult> AddTabSectionKeywordItemAsync(InpAddTabSectionKeywordItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Check SectionType
                {
                    var _Result = await CheckTabSectionTypeAsync(Input.ShowcaseTabSectionId, tblShowcaseTabSectionItemsEnum.Keyword);
                    if (_Result.IsSucceeded==false)
                        return new OperationResult().Failed(_Result.Message);
                }
                #endregion

                #region Check Has Keyword
                if (await _ShowcaseTabSectionItemRepository.Get.Where(a => a.TabSectionId==Input.ShowcaseTabSectionId.ToGuid()).AnyAsync(a => a.SectionType==tblShowcaseTabSectionItemsEnum.Keyword))
                    return new OperationResult().Failed("InKeywordSectionType,YouAreAllowedAddOneItem");

                #endregion

                #region Get sort num
                int _Sort = 0;
                {
                    _Sort= await _ShowcaseTabSectionItemRepository.Get.Where(a => a.TabSectionId==Input.ShowcaseTabSectionId.ToGuid()).CountAsync();
                }
                #endregion

                #region Add Item
                {
                    var tTabSectionItem = new tblShowcaseTabSectionItems
                    {
                        Id=new Guid().SequentialGuid(),
                        TabSectionId=Input.ShowcaseTabSectionId.ToGuid(),
                        SectionType=tblShowcaseTabSectionItemsEnum.Keyword,
                        Sort=_Sort,
                        tblSectionProductKeyword= new tblSectionProductKeyword
                        {
                            Id=new Guid().SequentialGuid(),
                            KeywordId=Input.KeywordId.ToGuid(),
                            CountFetch=Input.CountFetch,
                            OrderBy=(tblSectionProductKeywordOrderByEnum)Input.OrderBy
                        }
                    };

                    await _ShowcaseTabSectionItemRepository.AddAsync(tTabSectionItem, default, true);
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

        private async Task<OperationResult> CheckTabSectionTypeAsync(string TabSectionId, tblShowcaseTabSectionItemsEnum SectionType)
        {
            var qData = await _ShowcaseTabSectionItemRepository.Get
                                    .Where(a => a.TabSectionId==TabSectionId.ToGuid())
                                    .FirstOrDefaultAsync();

            if (qData!=null)
                if (qData.SectionType!=SectionType)
                    return new OperationResult().Failed(_Localizer["CantAddSectionItem, SectionTypeCanBe", _Localizer[qData.SectionType.ToString()]]);

            return new OperationResult().Succeeded();
        }

        public async Task<OperationResult> SortingSectionItemAsync(InpSortingSectionItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                #endregion

                var qListSlide = await _ShowcaseTabSectionItemRepository.Get.Where(a => a.TabSectionId==Input.TabSectionId.ToGuid()).OrderBy(a => a.Sort).ToListAsync();
                var qCurrentItem = qListSlide.Where(a => a.Id==Input.Id.ToGuid()).SingleOrDefault();
                if (qCurrentItem==null)
                    return new OperationResult().Failed("IdNotFound");

                int IndexOfCurrentItem = qListSlide.IndexOf(qCurrentItem);

                if (Input.Act==InpSortingSectionItemItem.Up)
                {
                    if (IndexOfCurrentItem!=0)
                    {
                        var PriveItem = qListSlide[IndexOfCurrentItem-1];

                        int OldPriveIndex = PriveItem.Sort;
                        PriveItem.Sort=IndexOfCurrentItem;
                        qCurrentItem.Sort=OldPriveIndex;

                        await _ShowcaseTabSectionItemRepository.UpdateAsync(PriveItem, default, false);
                        await _ShowcaseTabSectionItemRepository.UpdateAsync(qCurrentItem, default, false);

                        await _ShowcaseTabSectionItemRepository.SaveChangeAsync();
                    }
                }
                else if (Input.Act==InpSortingSectionItemItem.Down)
                {
                    if (IndexOfCurrentItem<(qListSlide.Count()-1))
                    {
                        var NextItem = qListSlide[IndexOfCurrentItem+1];

                        int OldPriveIndex = NextItem.Sort;
                        NextItem.Sort=IndexOfCurrentItem;
                        qCurrentItem.Sort=OldPriveIndex;

                        await _ShowcaseTabSectionItemRepository.UpdateAsync(NextItem, default, false);
                        await _ShowcaseTabSectionItemRepository.UpdateAsync(qCurrentItem, default, false);

                        await _ShowcaseTabSectionItemRepository.SaveChangeAsync();
                    }
                }


                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> RemoveSectionItemAsync(InpRemoveSectionItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseTabSectionItemRepository.Get
                                                     .Where(a => a.Id==Input.Id.ToGuid())
                                                     .SingleOrDefaultAsync();

                if (qData==null)
                    return new OperationResult().Failed("IdNotFound");

                await _ShowcaseTabSectionItemRepository.DeleteAsync(qData, default, true);
                return new OperationResult().Succeeded();

            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OutGetFreeItemForEdit> GetFreeItemForEditAsync(InpGetFreeItemForEdit Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseTabSectionItemRepository.Get
                                                .Where(a => a.Id==Input.SectionItemId.ToGuid())
                                                .Select(a => new OutGetFreeItemForEdit
                                                {
                                                    SectionItemId=a.Id.ToString(),
                                                    Name=a.tblSectionFreeItems.Name,
                                                    LstTranslate= a.tblSectionFreeItems.tblSectionFreeItemTranslate.Select(b => new OutGetFreeItemForEditTranslate
                                                    {
                                                        LangId=b.LangId.ToString(),
                                                        Title=b.Title,
                                                        Url=b.Url,
                                                        HtmlText=b.HtmlText,
                                                        FileId=b.FileId.ToString()
                                                    }).ToList()
                                                })
                                                .SingleOrDefaultAsync();

                if (qData==null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }

        public async Task<OperationResult> SaveEditFreeItemAsync(InpSaveEditFreeItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get Section FreeItem
                var qTabSectionItem = await _ShowcaseTabSectionItemRepository.Get
                                        .Where(a => a.Id==Input.SectionItemId.ToGuid())
                                        .SingleOrDefaultAsync();

                if (qTabSectionItem==null)
                    return new OperationResult().Failed("IdNotFound");
                #endregion

                #region Check duplicate name
                if (await _ShowcaseTabSectionItemRepository.Get.Where(a => a.tblSectionFreeItems.tblShowcaseTabSectionItems.TabSectionId==qTabSectionItem.TabSectionId).Where(a => a.Id!=Input.SectionItemId.ToGuid()).AnyAsync(a => a.tblSectionFreeItems.Name==Input.Name))
                    return new OperationResult().Failed("NameIsDuplicated");

                #endregion

                #region Check duplicate title
                var HasDuplicateTitle = (await _ShowcaseTabSectionFreeItemTranslateRepository.Get
                                                            .Where(a => a.tblSectionFreeItems.tblShowcaseTabSectionItems.TabSectionId==qTabSectionItem.TabSectionId)
                                                            .Where(a => a.tblSectionFreeItems.tblShowcaseTabSectionItems.Id!= Input.SectionItemId.ToGuid())
                                                            .Select(a => a.Title)
                                                            .ToListAsync())
                                                                    .Where(Title => Input.LstTranslate.Where(b => b.Title==Title).Any())
                                                                    .Any();
                if (HasDuplicateTitle)
                    return new OperationResult().Failed("TitleLangIsDuplicate");
                #endregion

                #region Remove Section Item
                {
                    await _ShowcaseTabSectionItemRepository.DeleteAsync(qTabSectionItem, default, true);
                }
                #endregion

                #region Add Section Item
                {
                    var tTabSectionFreeItem = new tblShowcaseTabSectionItems
                    {
                        Id=Input.SectionItemId.ToGuid(),
                        TabSectionId=qTabSectionItem.TabSectionId,
                        SectionType=tblShowcaseTabSectionItemsEnum.FreeItem,
                        Sort=qTabSectionItem.Sort,
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
                _Logger.Error(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }

        public async Task<OutGetProductItemForEdit> GetProductItemForEditAsync(InpGetProductItemForEdit Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseTabSectionItemRepository.Get
                                                .Where(a => a.Id==Input.SectionItemId.ToGuid())
                                                .Select(a => new OutGetProductItemForEdit
                                                {
                                                    SectionItemId=a.Id.ToString(),
                                                    ProductId=a.tblSectionProducts.ProductId.ToString()
                                                })
                                                .SingleOrDefaultAsync();

                if (qData==null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }

        public async Task<OperationResult> SaveEditProductItemAsync(InpSaveEditProductItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get Section Item
                var qTabSectionItem = await _ShowcaseTabSectionItemRepository.Get
                                                    .Where(a => a.Id==Input.SectionItemId.ToGuid())
                                                    .SingleOrDefaultAsync();

                if (qTabSectionItem==null)
                    return new OperationResult().Failed("IdNotFound");
                #endregion

                #region Check duplicate name
                if (await _ShowcaseTabSectionItemRepository.Get.Where(a => a.tblSectionFreeItems.tblShowcaseTabSectionItems.TabSectionId==qTabSectionItem.TabSectionId).Where(a => a.Id!=Input.SectionItemId.ToGuid()).AnyAsync(a => a.tblSectionProducts.ProductId==Input.ProductId.ToGuid()))
                    return new OperationResult().Failed("NameIsDuplicated");

                #endregion

                #region Remove Section Item
                {
                    await _ShowcaseTabSectionItemRepository.DeleteAsync(qTabSectionItem, default, true);
                }
                #endregion

                #region Add Section Item
                {
                    var tTabSectionFreeItem = new tblShowcaseTabSectionItems
                    {
                        Id=Input.SectionItemId.ToGuid(),
                        TabSectionId=qTabSectionItem.TabSectionId,
                        SectionType=tblShowcaseTabSectionItemsEnum.Product,
                        Sort=qTabSectionItem.Sort,
                        tblSectionProducts= new tblSectionProducts
                        {
                            Id=new Guid().SequentialGuid(),
                            ProductId=Input.ProductId.ToGuid()
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
                return new OperationResult().Failed(_Localizer["Error500"]);
            }
        }

        public async Task<OutGetCategoryItemForEdit> GetCategoryItemForEditAsync(InpGetCategoryItemForEdit Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseTabSectionItemRepository.Get
                                               .Where(a => a.Id==Input.SectionItemId.ToGuid())
                                               .Select(a => new OutGetCategoryItemForEdit
                                               {
                                                   SectionItemId=a.Id.ToString(),
                                                   CategoryId=a.tblSectionProductCategory.CategoryId.ToString(),
                                                   CountFetch=a.tblSectionProductCategory.CountFetch,
                                                   OrderBy=(OutGetCategoryItemForEditOrderByEnum)a.tblSectionProductCategory.OrderBy
                                               })
                                               .SingleOrDefaultAsync();

                if (qData==null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }

        public async Task<OperationResult> SaveEditCategoryItemAsync(InpSaveEditCategoryItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get Section Item
                var qTabSectionItem = await _ShowcaseTabSectionItemRepository.Get
                                                    .Where(a => a.Id==Input.SectionItemId.ToGuid())
                                                    .SingleOrDefaultAsync();

                if (qTabSectionItem==null)
                    return new OperationResult().Failed("IdNotFound");
                #endregion

                #region Check duplicate name
                if (await _ShowcaseTabSectionItemRepository.Get.Where(a => a.tblSectionFreeItems.tblShowcaseTabSectionItems.TabSectionId==qTabSectionItem.TabSectionId).Where(a => a.Id!=Input.SectionItemId.ToGuid()).AnyAsync(a => a.tblSectionProductCategory.CategoryId==Input.CategoryId.ToGuid()))
                    return new OperationResult().Failed("NameIsDuplicated");

                #endregion

                #region Remove Section Item
                {
                    await _ShowcaseTabSectionItemRepository.DeleteAsync(qTabSectionItem, default, true);
                }
                #endregion

                #region Add Section Item
                {
                    var tTabSectionFreeItem = new tblShowcaseTabSectionItems
                    {
                        Id=Input.SectionItemId.ToGuid(),
                        TabSectionId=qTabSectionItem.TabSectionId,
                        SectionType=tblShowcaseTabSectionItemsEnum.Category,
                        Sort=qTabSectionItem.Sort,
                        tblSectionProductCategory= new tblSectionProductCategory
                        {
                            Id=new Guid().SequentialGuid(),
                            CategoryId=Input.CategoryId.ToGuid(),
                            CountFetch=Input.CountFetch,
                            OrderBy=(tblSectionProductCategoryOrderByEnum)Input.OrderBy
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
                return new OperationResult().Failed(_Localizer["Error500"]);
            }
        }

        public async Task<OutGetKeywordItemForEdit> GetKeywordItemForEditAsync(InpGetKeywordItemForEdit Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseTabSectionItemRepository.Get
                                               .Where(a => a.Id==Input.SectionItemId.ToGuid())
                                               .Select(a => new OutGetKeywordItemForEdit
                                               {
                                                   SectionItemId=a.Id.ToString(),
                                                   KeywordId=a.tblSectionProductKeyword.KeywordId.ToString(),
                                                   CountFetch=a.tblSectionProductKeyword.CountFetch,
                                                   OrderBy=(OutGetKeywordItemForEditOrderByEnum)a.tblSectionProductKeyword.OrderBy
                                               })
                                               .SingleOrDefaultAsync();

                if (qData==null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }

        public async Task<OperationResult> SaveEditKeywordItemAsync(InpSaveEditKeywordItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get Section Item
                var qTabSectionItem = await _ShowcaseTabSectionItemRepository.Get
                                                    .Where(a => a.Id==Input.SectionItemId.ToGuid())
                                                    .SingleOrDefaultAsync();

                if (qTabSectionItem==null)
                    return new OperationResult().Failed("IdNotFound");
                #endregion

                #region Check duplicate name
                if (await _ShowcaseTabSectionItemRepository.Get.Where(a => a.tblSectionFreeItems.tblShowcaseTabSectionItems.TabSectionId==qTabSectionItem.TabSectionId).Where(a => a.Id!=Input.SectionItemId.ToGuid()).AnyAsync(a => a.tblSectionProductKeyword.KeywordId==Input.KeywordId.ToGuid()))
                    return new OperationResult().Failed("NameIsDuplicated");

                #endregion

                #region Remove Section Item
                {
                    await _ShowcaseTabSectionItemRepository.DeleteAsync(qTabSectionItem, default, true);
                }
                #endregion

                #region Add Section Item
                {
                    var tTabSectionFreeItem = new tblShowcaseTabSectionItems
                    {
                        Id=Input.SectionItemId.ToGuid(),
                        TabSectionId=qTabSectionItem.TabSectionId,
                        SectionType=tblShowcaseTabSectionItemsEnum.Keyword,
                        Sort=qTabSectionItem.Sort,
                        tblSectionProductKeyword= new tblSectionProductKeyword
                        {
                            Id=new Guid().SequentialGuid(),
                            KeywordId=Input.KeywordId.ToGuid(),
                            CountFetch=Input.CountFetch,
                            OrderBy=(tblSectionProductKeywordOrderByEnum)Input.OrderBy
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
                return new OperationResult().Failed(_Localizer["Error500"]);
            }
        }

    }
}
