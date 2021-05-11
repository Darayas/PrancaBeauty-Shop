using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Accesslevels;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Domin.Users.UserAgg.Contracts;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Localization;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Admin.Users.Components
{
    public class Compo_ChanageAccessLevelModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        private readonly IAccesslevelApplication _AccesslevelApplication;

        public Compo_ChanageAccessLevelModel(IUserApplication userApplication, IMsgBox msgBox, ILocalizer localizer, IAccesslevelApplication accesslevelApplication)
        {
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
            _AccesslevelApplication = accesslevelApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<JsonResult> OnGetReadAsync(string Text)
        {
            var qData = await _AccesslevelApplication.GetForChangeUserAccesssLevelAsync(Text);
            return new JsonResult(qData);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ChanageAccessLevel Input { get; set; }
    }
}
