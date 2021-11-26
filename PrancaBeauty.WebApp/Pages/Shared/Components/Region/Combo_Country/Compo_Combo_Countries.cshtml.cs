using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Countries;
using PrancaBeauty.Application.Contracts.Countries;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Region.Combo_Country
{
    public class Compo_Combo_CountriesModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly ICountryApplication _CountryApplication;
        public Compo_Combo_CountriesModel(ICountryApplication countryApplication, IMapper mapper)
        {
            _CountryApplication = countryApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.CountryId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string LangId, string Text)
        {
            var qData = await _CountryApplication.GetListForComboAsync(new InpGetListForCombo { LangId = LangId, Search = Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_Countries>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Countries Input { get; set; }
    }
}
