using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_CartWidget
{
    [Authorize]
    public class Compo_CartWidgetMainModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public viCompo_CartWidgetMain Input { get; set; }
        public vmCompo_CartWidgetMain Data { get; set; }
    }
}
