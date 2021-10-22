using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.Products.Components.Compo_Variants
{
    public class Compo_Variants_ItemsModel : PageModel
    {
        public void OnGet()
        {
        }

        [BindProperty]
        public viCompo_Variants_Items Input { get; set; }
    }
}
