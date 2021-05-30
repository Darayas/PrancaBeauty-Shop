using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Shared.Components
{
    public class Compo_Combo_LanguageModel : PageModel
    {
        private readonly ILanguageApplication _LanguageApplication;
        public Compo_Combo_LanguageModel(ILanguageApplication languageApplication)
        {
            _LanguageApplication = languageApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<JsonResult> OnGetReadAsync()
        {
            var qData = await _LanguageApplication.GetAllLanguageForSiteLangAsync();
            return new JsonResult(qData);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Language Input { get; set; }
    }
}
