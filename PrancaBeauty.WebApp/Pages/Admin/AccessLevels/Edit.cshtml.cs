using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Accesslevels;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.AccessLevels;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Admin.AccessLevels
{
    [Authorize(Roles = Roles.CanEditAccessLevel)]
    public class EditModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IAccesslevelApplication _AccesslevelApplication;
        private readonly IUserApplication _UserApplication;

        public EditModel(IAccesslevelApplication accesslevelApplication, IMsgBox msgBox, ILocalizer localizer, IUserApplication userApplication)
        {
            _AccesslevelApplication = accesslevelApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
            _UserApplication = userApplication;
        }

        public async Task<IActionResult> OnGetAsync(string ReturnUrl)
        {
            if (string.IsNullOrWhiteSpace(Input.Id))
                return StatusCode(400);

            var qData = await _AccesslevelApplication.GetForEditAsync(new InpGetForEdit { AccessLevelId = Input.Id });

            if (qData == null)
                return StatusCode(404);

            Input.Id = qData.Id;
            Input.Name = qData.Name;

            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/AccessLevel/List";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var Result = await _AccesslevelApplication.UpdateAsync(new InpUpdateAccessLevel()
            {
                Id = Input.Id,
                Name = Input.Name,
                Roles = Input.Roles
            });

            if (Result.IsSucceeded)
            {
                // ابدیت سطح دسترسی های کاربران
                var UpdateRolesResult = await _UserApplication.EditUsersRoleByAccIdAsync(new InpEditUsersRoleByAccId
                {
                    AccessLevelId = Input.Id,
                    Roles = Input.Roles.Select(a => new InpEditUsersRoleByAccId_Roles { Name = a }).ToList()
                });

                // ذخیره شناسه کاربران عضو سطح دسترسی جاری برای ابدیت توکن ها
                CacheUsersToRebuildToken.AddRange(await _UserApplication.GetUserIdsByAccIdAsync(new InpGetUserIdsByAccId { AccessLevelId = Input.Id }));

                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "GotoList()");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viEditAccessLevel Input { get; set; }
    }
}
