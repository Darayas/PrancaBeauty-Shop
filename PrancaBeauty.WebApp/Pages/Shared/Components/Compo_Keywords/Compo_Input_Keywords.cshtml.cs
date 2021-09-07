using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Keywords;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_Keywords
{
    public class Compo_Input_KeywordsModel : PageModel
    {
        private readonly IKeywordApplication _KeywordApplication;

        public Compo_Input_KeywordsModel(IKeywordApplication keywordApplication)
        {
            _KeywordApplication = keywordApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public viCompo_Input_Keywords Input { get; set; }
    }
}
