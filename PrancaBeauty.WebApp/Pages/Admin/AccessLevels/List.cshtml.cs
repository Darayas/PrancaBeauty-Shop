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
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.Application.Contracts.AccessLevels;
using Framework.Infrastructure;

namespace PrancaBeauty.WebApp.Pages.Admin.AccessLevels
{
    [Authorize(Roles = Roles.CanViewListAccessLevel)]
    public class ListModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IAccesslevelApplication _AccesslevelApplication;

        public ListModel(IAccesslevelApplication accesslevelApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _AccesslevelApplication = accesslevelApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request)
        {
            var qData = await _AccesslevelApplication.GetListForAdminPageAsync(new InpGetListForAdminPage
            {
                Title = null,
                PageNum = request.Page,
                Take = request.PageSize
            });


            var _DataGrid = qData.Item2.ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data = qData.Item2;

            return new JsonResult(_DataGrid);
        }

        public async Task<IActionResult> OnPostRemoveAsync(string Id)
        {
            if (!User.IsInRole(Roles.CanRemoveAccessLevel))
                return _MsgBox.FaildMsg(_Localizer["AccessDenied"]);

            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var Result = await _AccesslevelApplication.RemoveAsync(new InpRemove
            {
                Id = Id
            });

            if (Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "RefreshData()");
            }
            else
            {
                return _MsgBox.ModelStateMsg(_Localizer[Result.Message]);
            }
        }
    }
}
