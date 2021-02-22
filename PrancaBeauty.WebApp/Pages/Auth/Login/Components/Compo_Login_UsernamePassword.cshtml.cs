using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.Login.Components
{
    public class Compo_Login_UsernamePasswordModel : PageModel
    {
        public Compo_Login_UsernamePasswordModel()
        {

        }

        public IActionResult OnGet(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return Page();
        }

        [BindProperty]
        public viCompo_Login_UsernamePasswordModel Input { get; set; }
    }
}
