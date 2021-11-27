using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Framework.Application.Consts;
using Framework.Application.Services.Email;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Apps.Templates;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.Settings;
using PrancaBeauty.Application.Contracts.Templates;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.Login.Components
{
    public class Compo_Login_EmailLinkModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IEmailSender _EmailSender;
        private readonly IUserApplication _UserApplication;
        private readonly ILocalizer _Localizer;
        private readonly ISettingApplication _SettingApplication;
        private readonly ITemplateApplication _TemplateApplication;

        public Compo_Login_EmailLinkModel(IMsgBox msgBox, IUserApplication userApplication, ILocalizer localizer, ISettingApplication settingApplication, IEmailSender emailSender, ITemplateApplication templateApplication)
        {
            _MsgBox = msgBox;
            _UserApplication = userApplication;
            _Localizer = localizer;
            _SettingApplication = settingApplication;
            _EmailSender = emailSender;
            _TemplateApplication = templateApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            Thread.Sleep(3000);

            var Result = await _UserApplication.LoginByEmailLinkStep1Async(new InpLoginByEmailLinkStep1 { Email = Input.Email, IP = HttpContext.Connection.RemoteIpAddress.ToString() });
            if (Result.IsSucceeded)
            {
                string Token = (Result.Message + ", " + Input.RemmeberMe).AesEncrypt(AuthConst.SecretKey);
                string _Url = (await _SettingApplication.GetSettingAsync(new InpGetSetting { LangCode = CultureInfo.CurrentCulture.Name })).SiteUrl + $"/{CultureInfo.CurrentCulture.Parent.Name}/EmailLogin?Token={WebUtility.UrlEncode(Token)}";

                await _EmailSender.SendAsync(Input.Email, _Localizer["EmailLoginSubject"], await _TemplateApplication.GetEmailLoginTemplateAsync(new InpGetEmailLoginTemplate { LangCode = CultureInfo.CurrentCulture.Name, Url = _Url }));

                return _MsgBox.SuccessMsg(_Localizer["EmailLoginSent"], "location.href='/';");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty]
        public viCompo_Login_EmailLinkModel Input { get; set; }
    }
}
