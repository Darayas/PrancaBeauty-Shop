using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Home.Search.Components.Compo_Keyword
{
    public class Compo_KeywordSearchModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_KeywordSearch Input { get; set; }
        public vmCompo_KeywordSearch Data { get; set; }
    }
}
