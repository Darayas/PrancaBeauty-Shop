using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Localization;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Admin.Users
{
    [Authorize(Roles = Roles.CanViewListUsers)]
    public class ListModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UsersApplication;
        public ListModel(IMsgBox msgBox, ILocalizer localizer, IUserApplication usersApplication)
        {
            _MsgBox = msgBox;
            _Localizer = localizer;
            _UsersApplication = usersApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request, string FullName, string Email, string PhoneNumber, string FieldSort)
        {
            var qData = await _UsersApplication.GetListForAdminPageAsync(Email, PhoneNumber, FullName, FieldSort, request.Page, request.PageSize);


            var _DataGrid = qData.Item2
                                 .Select(a => new vmListUsers
                                 {
                                     Id = a.Id,
                                     FullName = a.FullName,
                                     AccessLevelName = a.AccessLevelName,
                                     Date = a.Date.ToString("yyyy/MM/dd HH:mm"),
                                     Email = a.Email,
                                     IsActive = a.IsActive,
                                     IsEmailConfirmed = a.IsEmailConfirmed,
                                     IsPhoneNumberConfirmed = a.IsPhoneNumberConfirmed,
                                     PhoneNumber = a.PhoneNumber
                                 })
                                 .ToDataSourceResult(request);
            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data = qData.Item2;

            return new JsonResult(_DataGrid);
        }
    }
}
