using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.RecoveryPassword.Components.RecoveryByEmail
{
    public class Compo_ResetPasswordModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        public Compo_ResetPasswordModel(IUserApplication userApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
        }


        public IActionResult OnGet()
        {
            Input.Token = Input.Token.Replace(" ", "+");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var Result = await _UserApplication.RecoveryPasswordByEmailStep2Async(new InpRecoveryPasswordByEmailStep2 { Token = Input.Token, NewPassword = Input.NewPassword });
            if (Result.IsSucceeded)
                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "GotoLoginPage()");
            else
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ResetPassword Input { get; set; }
    }
}
