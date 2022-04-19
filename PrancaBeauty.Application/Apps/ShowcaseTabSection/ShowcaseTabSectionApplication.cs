using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShowcaseTabSection
{
    public class ShowcaseTabSectionApplication : IShowcaseTabSectionApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseTabSectionRepository _ShowcaseTabSectionRepository;

        public ShowcaseTabSectionApplication(ILogger logger, IServiceProvider serviceProvider, IShowcaseTabSectionRepository showcaseTabSectionRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShowcaseTabSectionRepository=showcaseTabSectionRepository;
        }

        public async Task<(OutPagingData, List<OutGetListShowcaseTabSectionForAdminPage>)> GetListShowcaseTabSectionForAdminPageAsync(InpGetListShowcaseTabSectionForAdminPage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _ShowcaseTabSectionRepository.Get
                                       .Where(a => a.ShowcaseTabId==Input.ShowcaseTabId.ToGuid())
                                       .Select(a => new OutGetListShowcaseTabSectionForAdminPage
                                       {
                                           Id=a.Id.ToString(),
                                           ParentName=a.ParentId!=null ? a.tblShowcaseTabSectionsParent.Name : "",
                                           Name=a.Name,
                                           IsSlider=a.IsSlider,
                                           CountInSection=a.CountInSection,
                                           XlSize=a.XlSize,
                                           LgSize=a.LgSize,
                                           MdSize=a.MdSize,
                                           SmSize=a.SmSize,
                                           XsSize=a.XsSize,
                                       })
                                       .Where(a => Input.Name!=null ? a.Name.Contains(Input.Name) : true)
                                       .OrderBy(a => a.ParentName);

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

        public async Task<List<OutGetAllTabSectionForCombo>> GetAllTabSectionForComboAsync(InpGetAllTabSectionForCombo Input)
        {
            try
            {
                #region validatrions
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShowcaseTabSectionRepository.Get
                                                .Where(a => a.ShowcaseTabId==Input.ShowcaseTabId.ToGuid())
                                                .Select(a => new OutGetAllTabSectionForCombo
                                                {
                                                    Id=a.Id.ToString(),
                                                    Name=a.Name
                                                })
                                                .Where(a => Input.Name!=null ? a.Name.Contains(Input.Name) : true)
                                                .ToListAsync();

                return qData;
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

        public async Task<OperationResult> AddShowcaseTabSectionAsync(InpAddShowcaseTabSection Input)
        {
            try
            {
                #region validatrions
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Check Name duplicate
                {
                    if (await _ShowcaseTabSectionRepository.Get.Where(a => a.ShowcaseTabId==Input.ShowcaseTabId.ToGuid()).AnyAsync(a => a.Name==Input.Name))
                        return new OperationResult().Failed("NameIsDuplicate");
                }
                #endregion

                #region Add Tab Section
                {
                    var tAdd = new tblShowcaseTabSections
                    {
                        Id=new Guid().SequentialGuid(),
                        ShowcaseTabId=Input.ShowcaseTabId.ToGuid(),
                        ParentId=Input.ParentId!=null ? Input.ParentId.ToGuid() : null,
                        Name=Input.Name,
                        CountInSection=Input.CountInSection,
                        IsSlider=Input.IsSlider,
                        LgSize=Input.LgSize,
                        MdSize=Input.MdSize,
                        SmSize=Input.SmSize,
                        XlSize=Input.XlSize,
                        XsSize=Input.XsSize
                    };

                    await _ShowcaseTabSectionRepository.AddAsync(tAdd, default, true);
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
