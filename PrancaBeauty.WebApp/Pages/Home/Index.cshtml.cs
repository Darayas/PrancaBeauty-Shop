using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Localization;

namespace PrancaBeauty.WebApp.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly ILocalizer Localizer;
        public IndexModel(ILocalizer _Localizer)
        {
            Localizer = _Localizer;
        }
        public void OnGet()
        {
            string Msg = Localizer["Hello"];
        }
    }
}
