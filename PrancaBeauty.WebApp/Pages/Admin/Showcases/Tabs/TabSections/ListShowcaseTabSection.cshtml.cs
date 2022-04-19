using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ShowcaseTabSection;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases.Tabs.TabSections
{
    [Authorize(Roles = Roles.CanViewListShowcaseTabSections)]
    public class ListShowcaseTabSectionModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly IShowcaseTabSectionApplication _ShowcaseTabSectionApplication;

        public ListShowcaseTabSectionModel(ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer, IMsgBox msgBox, IMapper mapper, IShowcaseTabSectionApplication showcaseTabSectionApplication)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _Localizer=localizer;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _ShowcaseTabSectionApplication=showcaseTabSectionApplication;
        }

        public IActionResult OnGet(viGetListShowcaseTabSection Input, string ReturnUrl = null)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                ViewData["ShowcaseId"]=Input.ShowcaseId;
                ViewData["ShowcaseTabId"]=Input.ShowcaseTabId;
                ViewData["ReturnUrl"] = ReturnUrl??$"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/ShowcaseTabs/List/{Input.ShowcaseId}";

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request)
        {
            var qData = await _ShowcaseTabSectionApplication.GetListShowcaseTabSectionForAdminPageAsync(new InpGetListShowcaseTabSectionForAdminPage
            {
                ShowcaseTabId=Input.ShowcaseTabId,
                Name = Input.Name,
                Page = request.Page,
                Take = request.PageSize
            });

            var _DataGrid = qData.Item2.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data =_Mapper.Map<List<vmListShowcaseTabSection>>(qData.Item2);

            return new JsonResult(_DataGrid);
        }

        [BindProperty]
        public viListShowcaseTabSection Input { get; set; }
    }
}
