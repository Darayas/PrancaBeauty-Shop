using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Framework.Application.Consts;
using Framework.Application.Services.Email;
using Framework.Common.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Apps.Templates;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Localization;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IEmailSender _EmailSender;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        private readonly ITemplateApplication _TemplateApplication;
        private readonly ISettingApplication _SettingApplication;

        public RegisterModel(IUserApplication UserApplication, IEmailSender EmailSender, ILocalizer Localizer, ITemplateApplication TemplateApplication, ISettingApplication settingApplication)
        {
            _UserApplication = UserApplication;
            _Localizer = Localizer;
            _EmailSender = EmailSender;

            _TemplateApplication = TemplateApplication;
            _SettingApplication = settingApplication;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _UserApplication.RemoveUnConfirmedUserAsync(Input.Email);

            var Result = await _UserApplication.AddUserAsync(new InpAddUser
            {
                Email = Input.Email,
                PhoneNumber = Input.PhoneNumber,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Password = Input.Password
            });

            if (Result.IsSucceeded)
            {
                if (Result.Code == 1)
                {
                    #region ارسال ایمیل تایید
                    {
                        string UserId = Result.Message;
                        string Token = await _UserApplication.GenerateEmailConfirmationTokenAsync(UserId);
                        string EncToken = $"{UserId}, {Token}".AesEncrypt(AuthConst.SecretKey);

                        string SiteUrl = (await _SettingApplication.GetSettingAsync(CultureInfo.CurrentCulture.Name)).SiteUrl;

                        string _Url = $"{SiteUrl}/{CultureInfo.CurrentCulture.Parent.Name}/Auth/EmailConfirmation?Token={WebUtility.UrlEncode(EncToken)}";

                        await _EmailSender.SendAsync(Input.Email, _Localizer["RegistrationEmailSubject"], await _TemplateApplication.GetEmailConfirmationTemplateAsync(CultureInfo.CurrentCulture.Name, _Url));
                    }
                    #endregion

                    return Redirect($"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/AccountCreate");
                }
                else
                    return Redirect($"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/AccountCreate");
            }
            else
            {
                foreach (var item in Result.Message.Split(", "))
                {
                    ModelState.AddModelError("", item);
                }

                return Page();
            }
        }

        [BindProperty]
        public viRegisterModel Input { get; set; }
    }
}
