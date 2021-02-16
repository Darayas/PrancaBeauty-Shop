using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Framework.Application.Consts;
using Framework.Application.Services.Email;
using Framework.Common.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public RegisterModel(IUserApplication UserApplication, IEmailSender EmailSender, ILocalizer Localizer)
        {
            _UserApplication = UserApplication;
            _Localizer = Localizer;
            _EmailSender = EmailSender;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

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

                        string _Url = $"/Auth/EmailConfirmation?Token={WebUtility.UrlEncode(EncToken)}";

                        await _EmailSender.SendAsync(Input.Email, _Localizer["RegistrationEmailSubject"], $"<a href=\"{_Url}\">Click!!!</a>");
                    }
                    #endregion
                }
                else
                    return Redirect("/Auth/UserCreatedSuccessfully");
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
