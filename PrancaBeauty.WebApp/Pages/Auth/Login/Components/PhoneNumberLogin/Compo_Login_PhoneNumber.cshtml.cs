using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Localization;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.Login.Components.PhoneNumberLogin
{
    public class Compo_Login_PhoneNumberModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        public Compo_Login_PhoneNumberModel(IUserApplication userApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
        }
        public IActionResult OnGet(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? "/Auth/User/Index";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var Result = await _UserApplication.LoginByPhoneNumberStep1Async(Input.PhoneNumber);
            if (Result.IsSucceeded)
            {

            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty]
        public viCompo_Login_PhoneNumberModel Input { get; set; }
    }
}
