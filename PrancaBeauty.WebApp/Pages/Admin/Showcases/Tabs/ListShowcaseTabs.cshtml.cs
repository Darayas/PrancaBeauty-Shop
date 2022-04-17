using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ShowcaseTabs;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases.Tabs
{
    [Authorize(Roles = Roles.CanViewListShowcaseTab)]
    public class ListShowcaseTabsModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly IShowcaseTabApplication _ShowcaseTabApplication;

        public ListShowcaseTabsModel(ILogger logger, IServiceProvider serviceProvider, IMsgBox msgBox, IMapper mapper, IShowcaseTabApplication showcaseTabApplication)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _MsgBox=msgBox;
            _Mapper=mapper;
            _ShowcaseTabApplication=showcaseTabApplication;
        }

        public IActionResult OnGet(viGetListShowcaseTabs Input, string ReturnUrl = null)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                ViewData["Id"]=Input.ShowcaseId;
                ViewData["ReturnUrl"]=ReturnUrl??$"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/Showcase/List";

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string LangId)
        {
            var qData = await _ShowcaseTabApplication.GetListShowcaseTabForAdminPageAsync(new InpGetListShowcaseTabForAdminPage
            {
                ShowcaseId=Input.ShowcaseId,
                LangId=LangId,
                Title = Input.Title,
                Page = request.Page,
                Take = request.PageSize
            });

            var _DataGrid = qData.Item2.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.PagingData.CountAllItem;
            _DataGrid.Data =_Mapper.Map<List<vmListShowcaseTabs>>(qData.LstData);

            return new JsonResult(_DataGrid);
        }


        [BindProperty]
        public viListShowcaseTabs Input { get; set; }
    }
}
