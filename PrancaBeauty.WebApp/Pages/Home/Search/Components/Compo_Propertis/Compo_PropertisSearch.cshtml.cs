using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Home.Search.Components.Compo_Propertis
{
    public class Compo_PropertisSearchModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_PropertisSearch Input { get; set; }
        public vmCompo_PropertisSearch Data { get; set; }
    }
}
