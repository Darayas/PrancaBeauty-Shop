using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Admin.Categories
{
    [Authorize(Roles = Roles.CanAddCategory)]
    public class AddModel : PageModel
    {
        private readonly ICategoryApplication _CategoryApplication;
        private readonly ILanguageApplication _LanguageApplication;
        public AddModel(ICategoryApplication categoryApplication, ILanguageApplication languageApplication)
        {
            _CategoryApplication = categoryApplication;
            _LanguageApplication = languageApplication;
        }

        public async Task<IActionResult> OnGetAsync(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/Category/List";

            var qLang = await _LanguageApplication.GetAllLanguageForSiteLangAsync();

            Input = new viAddCategory() { LstTranslate = new List<viAddCategory_Translate>() };

            foreach (var item in qLang)
            {
                Input.LstTranslate.Add(new viAddCategory_Translate()
                {
                    LangId = item.Id
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

        [BindProperty]
        public viAddCategory Input { get; set; }
    }
}
