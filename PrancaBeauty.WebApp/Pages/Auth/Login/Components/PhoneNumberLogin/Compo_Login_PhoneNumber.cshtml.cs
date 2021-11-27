using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application.Services.Sms;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.Login.Components.PhoneNumberLogin
{
    public class Compo_Login_PhoneNumberModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ISmsSender _SmsSender;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        public Compo_Login_PhoneNumberModel(IUserApplication userApplication, IMsgBox msgBox, ILocalizer localizer, ISmsSender smsSender)
        {
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
            _SmsSender = smsSender;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var Result = await _UserApplication.LoginByPhoneNumberStep1Async(new InpLoginByPhoneNumberStep1 { PhoneNumber = Input.PhoneNumber });
            if (Result.IsSucceeded)
            {
                var IsSend = _SmsSender.SendLoginCode(Input.PhoneNumber, Result.Message);
                if (IsSend)
                    return _MsgBox.SuccessMsg(_Localizer["LoginCodeIsSent"], "GotoOtpPage()");
                else
                    return _MsgBox.FaildMsg(_Localizer["SmsSenderNotRespond"]);
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
