using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ShowcaseTabs;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
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
            if (!ModelState.IsValid)
                return StatusCode(400);
            ViewData["Id"]=Input.Id;
            ViewData["ReturnUrl"]=ReturnUrl??$"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/Showcase/List";

            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string LangId)
        {
            //    var qData = await _ShowcaseApplication.GetListShowcaseForAdminPageAsync(new InpGetListShowcaseForAdminPage
            //    {
            //        LangId=LangId,
            //        CountryId=Input.CountryId,
            //        Title = Input.Title,
            //        Page = request.Page,
            //        Take = request.PageSize
            //    });


            //    var _DataGrid = qData.Item2.ToDataSourceResult(request);
            //    _DataGrid.Total = (int)qData.Item1.CountAllItem;
            //    _DataGrid.Data =_Mapper.Map<List<vmListShowcases>>(qData.Item2);

            //return new JsonResult(_DataGrid);
            return default;
        }


        [BindProperty(SupportsGet = true)]
        public viListShowcaseTabs Input { get; set; }
    }
}
