using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Countries;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Region.Combo_Country
{
    public class Compo_Combo_CountriesModel : PageModel
    {
        private readonly ICountryApplication _CountryApplication;
        public Compo_Combo_CountriesModel(ICountryApplication countryApplication)
        {
            _CountryApplication = countryApplication;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.CountryId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string LangId, string Text)
        {
            var qData = await _CountryApplication.GetListForComboAsync(LangId, Text);
            return new JsonResult(qData);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Countries Input { get; set; }
        public vmCompo_Combo_Countries Data { get; set; }
    }
}
