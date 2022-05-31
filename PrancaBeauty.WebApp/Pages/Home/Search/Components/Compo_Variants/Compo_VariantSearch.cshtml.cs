using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Home.Search.Components.Compo_Variants
{
    public class Compo_VariantSearchModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_VariantSearch Input { get; set; }
        public List<vmCompo_VariantSearch> Data { get; set; }
    }
}
