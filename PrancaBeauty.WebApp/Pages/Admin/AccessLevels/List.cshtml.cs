using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrancaBeauty.WebApp.Pages.Admin.AccessLevels
{
    public class ListModel : PageModel
    {

        public IActionResult OnGet()
        {
            return Page();
        }

        
    }
}
