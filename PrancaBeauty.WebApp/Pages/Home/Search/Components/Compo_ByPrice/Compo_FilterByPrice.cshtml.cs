using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Home.Search.Components.Compo_ByPrice
{
    public class Compo_FilterByPriceModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_FilterByPrice Input { get; set; }
    }
}
