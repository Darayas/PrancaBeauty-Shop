using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.Products.Components.Compo_Variants
{
    public class Compo_VariantsModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.VariantId";
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Variants Input { get; set; }
    }
}
