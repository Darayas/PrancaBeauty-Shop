using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application.Services.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IEmailSender _EmailSender;
        private readonly IUserApplication _UserApplication;

        public RegisterModel(IUserApplication UserApplication, IEmailSender Sender)
        {
            _UserApplication = UserApplication;
            _Sender = Sender;
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        public async Task<IActionResult> OnPost()
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
                // ارسال ایمیل تایید

                await _EmailSender.SendAsync("", "", "");

                return Page();
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
