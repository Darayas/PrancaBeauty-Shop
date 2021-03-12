using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.Login.Components.PhoneNumberLogin
{
    public class Compo_Login_PhoneNumberModel : PageModel
    {
        private readonly IUserApplication _UserApplication;
        public Compo_Login_PhoneNumberModel(IUserApplication userApplication)
        {
            _UserApplication = userApplication;
        }
        public IActionResult OnGet(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? "/Auth/User/Index";
            return Page();
        }

        [BindProperty]
        public viCompo_Login_PhoneNumberModel Input { get; set; }
    }
}
