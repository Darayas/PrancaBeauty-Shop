using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.Products.Components.Compo_Properties
{
    public class Compo_PropertiesModel : PageModel
    {
        public Compo_PropertiesModel()
        {

        }

        public void OnGet()
        {
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Properties Input { get; set; }
    }
}
