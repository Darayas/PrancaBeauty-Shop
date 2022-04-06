using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.RecoveryPassword
{
    public class RecoveryPasswordModel : PageModel
    {
        public void OnGet()
        {
        }

        [BindProperty(SupportsGet = true)]
        public viRecoveryPassword Input { get; set; }
    }
}
