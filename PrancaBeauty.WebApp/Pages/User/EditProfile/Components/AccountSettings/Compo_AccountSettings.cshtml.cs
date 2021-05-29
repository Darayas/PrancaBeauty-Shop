using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.EditProfile.Components.AccountSettings
{
    [Authorize]
    public class Compo_AccountSettingsModel : PageModel
    {
        public Compo_AccountSettingsModel()
        {

        }

        public async Task<IActionResult> OnGetAsync()
        {
            string UserId = User.GetUserDetails().UserId;
            return Page();
        }

        public viCompo_AccountSettings Input { get; set; }
    }
}
