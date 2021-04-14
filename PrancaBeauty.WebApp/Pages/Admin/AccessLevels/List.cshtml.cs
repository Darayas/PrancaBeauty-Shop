using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Accesslevels;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Authorization;
using PrancaBeauty.WebApp.Authentication;

namespace PrancaBeauty.WebApp.Pages.Admin.AccessLevels
{
    [Authorize(Roles = Roles.CanViewListAccessLevel)]
    public class ListModel : PageModel
    {
        private readonly IAccesslevelApplication _AccesslevelApplication;

        public ListModel(IAccesslevelApplication accesslevelApplication)
        {
            _AccesslevelApplication = accesslevelApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request)
        {
            var qData = await _AccesslevelApplication.GetListForAdminPageAsync(null, request.Page, request.PageSize);


            var _DataGrid = qData.Item2.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data = qData.Item2;

            return new JsonResult(_DataGrid);
        }

    }
}
