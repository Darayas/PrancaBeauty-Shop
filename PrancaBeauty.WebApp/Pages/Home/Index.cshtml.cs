using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Localization;

namespace PrancaBeauty.WebApp.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _Logger;
        public IndexModel(ILogger logger)
        {
            _Logger = logger;
        }
        public async Task<IActionResult> OnGetAsync(string LangId, string CountryId, string CurrencyId)
        {
            ViewData["LangId"]= LangId;
            ViewData["CountryId"]= CountryId;
            ViewData["CurrencyId"]= CurrencyId;
            return Page();
        }
    }
}
