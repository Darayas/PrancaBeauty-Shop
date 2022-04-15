using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Showcases;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.WebApp.Authentication;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases
{
    [Authorize(Roles = Roles.CanViewListShowcase)]
    public class ListShowcasesModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseApplication _ShowcaseApplication;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string _LangId)
        {
            var qData = await _ShowcaseApplication.GetListShowcaseForAdminPageAsync(new InpGetListShowcaseForAdminPage
            {
                LangId=_LangId,
                CountryId=Input.CountryId,
                Title = Input.Title,
                Page = request.Page,
                Take = request.PageSize
            });


            var _DataGrid = qData.Item2.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data = qData.Item2;

            return new JsonResult(_DataGrid);
        }


        [BindProperty(SupportsGet = true)]
        public viListShowcases Input { get; set; }
    }
}
