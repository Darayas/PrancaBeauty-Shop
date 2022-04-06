using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Currency;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Currency;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Languages;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_Price
{
    public class Compo_Input_PriceModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly ICurrencyApplication _CurrencyApplication;
        private readonly ILanguageApplication _LanguageApplication;
        public Compo_Input_PriceModel(ICurrencyApplication currencyApplication, ILanguageApplication languageApplication, IMapper mapper)
        {
            _CurrencyApplication = currencyApplication;
            _LanguageApplication = languageApplication;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string _CountryId = await _LanguageApplication.GetCountryIdByLangIdAsync(new InpGetCountryIdByLangId { LangId = Input.LangId });
            var qCurrency = await _CurrencyApplication.GetMainByCountryIdAsync(new InpGetMainByCountryId { LangId = Input.LangId, CountryId = _CountryId });

            Data = _Mapper.Map<vmCompo_Input_Price>(qCurrency);

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Input_Price Input { get; set; }
        public vmCompo_Input_Price Data { get; set; }
    }
}
