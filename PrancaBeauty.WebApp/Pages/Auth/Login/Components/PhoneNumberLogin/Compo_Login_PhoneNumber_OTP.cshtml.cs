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
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.Login.Components.PhoneNumberLogin
{
    public class Compo_Login_PhoneNumber_OTPModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IJWTBuilder _JwtBuilder;
        private readonly ISmsSender _SmsSender;
        private readonly IUserApplication _UserApplication;
        public Compo_Login_PhoneNumber_OTPModel(IMsgBox msgBox, IUserApplication userApplication, ILocalizer localizer, IJWTBuilder jwtBuilder, ISmsSender smsSender)
        {
            _MsgBox = msgBox;
            _UserApplication = userApplication;
            _Localizer = localizer;
            _JwtBuilder = jwtBuilder;
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

            var Result = await _UserApplication.LoginByPhoneNumberStep2Async(new InpLoginByPhoneNumberStep2 { PhoneNumber = Input.PhoneNumber, Code = Input.Code });
            if (Result.IsSucceeded)
            {
                string GeneratedToken = await _JwtBuilder.CreateTokenAync(Result.Message);
                Response.CreateAuthCookie(GeneratedToken, Input.RemmeberMe);

                return new JsResult("GotoReturnUrl()");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        public async Task<IActionResult> OnPostResendAsync(string PhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
                return _MsgBox.ModelStateMsg(_Localizer["PhoneNumberCantBeNull"]);

            var qUser = await _UserApplication.GetUserByPhoneNumberAsync(new InpGetUserByPhoneNumber { PhoneNumber = PhoneNumber });
            if (qUser == null)
                return _MsgBox.ModelStateMsg(_Localizer["PhoneNumberIsInvalid"]);

            var Result = await _UserApplication.ReCreatePasswordAsync(new InpReCreatePassword { UserId = qUser.Id.ToString() });
            if (Result.IsSucceeded)
            {
                var IsSend = _SmsSender.SendLoginCode(Input.PhoneNumber, Result.Message);
                if (IsSend)
                    return _MsgBox.SuccessMsg(_Localizer["LoginCodeIsSent"], "StartTimer()");
                else
                    return _MsgBox.FaildMsg(_Localizer["SmsSenderNotRespond"]);
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Login_PhoneNumber_OTPModel Input { get; set; }
    }
}
