using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.EditProfile.Components.AccountSettings
{
    [Authorize]
    public class Compo_AccountSettingsModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IUserApplication _UserApplication;
        public Compo_AccountSettingsModel(IMsgBox msgBox, IUserApplication userApplication)
        {
            _MsgBox = msgBox;
            _UserApplication = userApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var qData = await _UserApplication.GetUserDetailsForAccountSettingsAsync(User.GetUserDetails().UserId);

            if (qData == null)
                throw new Exception();

            Input = new viCompo_AccountSettings()
            {
                LangId = qData.LangId,
                Email = qData.Email,
                FirstName = qData.FirstName,
                LastName = qData.LastName,
                PhoneNumber = qData.PhoneNumber,
                BirthDate = qData.BirthDate.HasValue ? qData.BirthDate.Value.ToString("yyyy/MM/dd") : DateTime.Now.ToString("yyyy/MM/dd")
            };

            return Page();
        }

        [BindProperty]
        public viCompo_AccountSettings Input { get; set; }
    }
}
