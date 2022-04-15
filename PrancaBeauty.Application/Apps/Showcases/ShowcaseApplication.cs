using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Showcases
{
    public class ShowcaseApplication : IShowcaseApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseRepository _ShowcaseRepository;

        public ShowcaseApplication(ILogger logger, IServiceProvider serviceProvider, IShowcaseRepository showcaseRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShowcaseRepository=showcaseRepository;
        }

        public async Task<(OutPagingData, List<OutGetListShowcaseForAdminPage>)> GetListShowcaseForAdminPageAsync(InpGetListShowcaseForAdminPage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _ShowcaseRepository.Get
                                               .Select(a => new OutGetListShowcaseForAdminPage
                                               {
                                                   Id=a.Id.ToString(),
                                                   Title=a.tblShowcasesTranslates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single(),
                                                   CountyTitle=a.CountryId!=null ? a.tblCountry.tblCountries_Translates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single() : "",
                                                   IsFullWidth=a.IsFullWidth,
                                                   IsEnable=a.IsEnable,
                                                   IsActive=a.IsActive,
                                                   StartDate=a.StartDate,
                                                   EndDate=a.EndDate,
                                                   Sort=a.Sort
                                               })
                                               .Where(a => Input.Title!=null ? a.Title.Contains(Input.Title) : true)
                                               .OrderBy(a => a.Sort);

                var _PagingData = PagingData.Calc(await qData.LongCountAsync(), Input.Page, Input.Take);
                return (_PagingData, await qData.Skip((int)_PagingData.Skip).Take(_PagingData.Take).ToListAsync());
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
    }
}
