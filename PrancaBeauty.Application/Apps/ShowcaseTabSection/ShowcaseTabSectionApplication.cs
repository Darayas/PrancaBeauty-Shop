using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Contracts;
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
    }
}
