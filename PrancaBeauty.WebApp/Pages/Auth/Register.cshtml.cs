using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        public RegisterModel()
        {

        }

        public IActionResult OnGet()
        {

            return Page();
        }

        public IActionResult OnPost()
        {

            return Page();
        }

        public viRegisterModel Input { get; set; }
    }
}
