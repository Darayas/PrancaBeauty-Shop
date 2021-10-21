using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductPropertis;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.User.Products.Components.Compo_Properties
{
    public class Compo_PropertiesModel : PageModel
    {
        private readonly IProductPropertisApplication _ProductPropertisApplication;
        public Compo_PropertiesModel(IProductPropertisApplication productPropertisApplication)
        {
            _ProductPropertisApplication = productPropertisApplication;
        }

        public void OnGet()
        {

        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Properties Input { get; set; }
        public List<vmCompo_Properties> Data { get; set; }

    }
}
