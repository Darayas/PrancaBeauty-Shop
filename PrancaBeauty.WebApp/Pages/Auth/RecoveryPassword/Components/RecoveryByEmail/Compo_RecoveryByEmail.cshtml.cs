using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.RecoveryPassword.Components.RecoveryByEmail
{
    public class Compo_RecoveryByEmailModel : PageModel
    {
        private readonly IUserApplication _UserApplication;
        public Compo_RecoveryByEmailModel(IUserApplication userApplication)
        {
            _UserApplication = userApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        
        public IActionResult OnPostAsync()
        {
            
        }

        [BindProperty]
        public viCompo_RecoveryByEmail Input { get; set; }
    }
}
