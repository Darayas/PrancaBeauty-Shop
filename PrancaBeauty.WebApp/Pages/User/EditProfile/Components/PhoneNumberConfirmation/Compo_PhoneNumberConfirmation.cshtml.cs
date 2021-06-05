using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application.Services.Sms;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.EditProfile.Components.PhoneNumberConfirmation
{
    public class Compo_PhoneNumberConfirmationModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly ISmsSender _SmsSender;
        private readonly IUserApplication _UserApplication;
        public Compo_PhoneNumberConfirmationModel(IMsgBox msgBox, IUserApplication userApplication, ILocalizer localizer, ISmsSender smsSender)
        {
            _MsgBox = msgBox;
            _UserApplication = userApplication;
            _Localizer = localizer;
            _SmsSender = smsSender;
        }
        public IActionResult OnGet()
        {

            return Page();
        }

        public async Task<IActionResult> OnPostResendAsync(string PhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
                return _MsgBox.ModelStateMsg(_Localizer["PhoneNumberCantBeNull"]);

            var qUser = await _UserApplication.GetUserByPhoneNumberAsync(PhoneNumber);
            if (qUser == null)
                return _MsgBox.ModelStateMsg(_Localizer["PhoneNumberIsInvalid"]);

            var Result = await _UserApplication.ReCreatePasswordAsync(qUser);
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

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_PhoneNumberConfirmation Input { get; set; }
    }
}
