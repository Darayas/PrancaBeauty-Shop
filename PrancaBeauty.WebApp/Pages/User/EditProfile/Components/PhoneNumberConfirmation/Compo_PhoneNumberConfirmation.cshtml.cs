using Framework.Application.Services.Sms;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using System.Threading.Tasks;

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
        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrWhiteSpace(Input.PhoneNumber))
                return StatusCode(400);

            var Result = await _UserApplication.ReSendSmsCodeAsync(new InpReSendSmsCode { PhoneNumber = Input.PhoneNumber });
            if (Result.IsSucceeded)
                return Page();
            else
            {
                ViewData["ErrMsg"] = _Localizer["PhoneNumberConfirmationCodeNotSent"];
                return Page();
            }
        }

        public async Task<IActionResult> OnPostResendAsync(string PhoneNumber)
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
                return _MsgBox.ModelStateMsg(_Localizer["PhoneNumberCantBeNull"]);

            var Result = await _UserApplication.ReSendSmsCodeAsync(new InpReSendSmsCode { PhoneNumber = PhoneNumber });
            if (Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "StartTimer()");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var UserId = User.GetUserDetails().UserId;
            var Result = await _UserApplication.PhoneConfirmationBySmsCodeAsync(new InpPhoneConfirmationBySmsCode { UserId = UserId, PhoneNumber = Input.PhoneNumber, Code = Input.Code });
            if (Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "ReloadPage()");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_PhoneNumberConfirmation Input { get; set; }
    }
}
