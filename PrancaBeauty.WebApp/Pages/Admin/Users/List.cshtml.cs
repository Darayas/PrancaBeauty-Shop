using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
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

        public async Task<IActionResult> OnPostReadDataAsync([DataSourceRequest] DataSourceRequest request)
        {
            var qData = await _UsersApplication.GetListForAdminPageAsync(new InpGetListForAdminPage
            {
                Email = Input.Email,
                FullName = Input.FullName,
                PhoneNumber = Input.PhoneNumber,
                Sort = Input.FieldSort,
                PageNum = request.Page,
                Take = request.PageSize
            });

            var Items = qData.Item2
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
                                 PhoneNumber = a.PhoneNumber,
                                 AccessLevelId = a.AccessLevelId,
                                 ImgUrl = a.ImgUrl
                             });

            var _DataGrid = Items.ToDataSourceResult(request);

            _DataGrid.Total = (int)qData.Item1.CountAllItem;
            _DataGrid.Data = Items;

            return new JsonResult(_DataGrid);
        }

        public async Task<IActionResult> OnPostRemoveAsync(string Id)
        {
            if (!User.IsInRole(Roles.CanRemoveUsers))
                return _MsgBox.FaildMsg(_Localizer["AccessDenied"]);

            var Result = await _UsersApplication.RemoveUserAsync(new InpRemoveUser { UserId = Id });
            if (Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "RefreshData()");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message.Replace(" | ", "<br/>")]);
            }
        }

        public async Task<IActionResult> OnPostChanageStatusAsync(string Id)
        {
            if (!User.IsInRole(Roles.CanChangeUsersStatus))
                return _MsgBox.FaildMsg(_Localizer["AccessDenied"]);

            var Result = await _UsersApplication.ChangeUserStatusAsync(new InpChangeUserStatus { UserId = Id, SelfUserId = User.GetUserDetails().UserId });
            if (Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "RefreshData()");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viListUsers Input { get; set; }
    }
}
