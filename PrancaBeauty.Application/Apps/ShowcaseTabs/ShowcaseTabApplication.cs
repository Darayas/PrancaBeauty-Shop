using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ShowcaseTabs
{
    public class ShowcaseTabApplication : IShowcaseTabApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseTabsRepository _ShowcaseTabsRepository;

        public ShowcaseTabApplication(ILogger logger, IServiceProvider serviceProvider, IShowcaseTabsRepository showcaseTabsRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShowcaseTabsRepository=showcaseTabsRepository;
        }

        public async Task<(OutPagingData PagingData, List<OutGetListShowcaseTabForAdminPage> LstData)> GetListShowcaseTabForAdminPageAsync(InpGetListShowcaseTabForAdminPage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _ShowcaseTabsRepository.Get
                                        .Where(a => a.ShowcaseId==Input.ShowcaseId.ToGuid())
                                        .Select(a => new OutGetListShowcaseTabForAdminPage
                                        {
                                            Id=a.Id.ToString(),
                                            Name=a.Name,
                                            Title=a.tblShowcaseTabTranslates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single(),
                                            Sort=a.Sort,
                                            IsEnable=a.IsEnable,
                                            IsActive=a.IsActive,
                                            StartDate=a.StartDate,
                                            EndDate=a.EndDate
                                        })
                                        .OrderBy(a => a.Sort);

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

        public async Task<OperationResult> AddShowcaseTabAsync(InpAddShowcaseTab Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Check name duplicate

                #endregion

                #region Check title duplicate

                #endregion

                #region Get sorting num

                #endregion

                #region Add showcaseTab

                #endregion

                return new OperationResult().Succeeded();

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
    }
}
