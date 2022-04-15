using AutoMapper;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Showcases;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Showcases
{
    [Authorize(Roles = Roles.CanViewListShowcase)]
    public class ListShowcasesModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseApplication _ShowcaseApplication;

        public ListShowcasesModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IShowcaseApplication showcaseApplication)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _ShowcaseApplication=showcaseApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string LangId)
        {
            var qData = await _ShowcaseApplication.GetListShowcaseForAdminPageAsync(new InpGetListShowcaseForAdminPage
            {
                LangId=LangId,
                CountryId=Input.CountryId,
                Title = Input.Title,
                Page = request.Page,
                Take = request.PageSize
            });


            var _DataGrid = qData.Item2.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data =_Mapper.Map<List<vmListShowcases>>(qData.Item2);

            return new JsonResult(_DataGrid);
        }


        [BindProperty(SupportsGet = true)]
        public viListShowcases Input { get; set; }
    }
}
