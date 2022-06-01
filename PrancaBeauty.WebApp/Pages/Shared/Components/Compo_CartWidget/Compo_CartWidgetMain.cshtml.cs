using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Carts;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_CartWidget
{
    [Authorize]
    public class Compo_CartWidgetMainModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ICartApplication _CartApplication;

        public Compo_CartWidgetMainModel(ILogger logger, IServiceProvider serviceProvider, ICartApplication cartApplication)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _CartApplication=cartApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public viCompo_CartWidgetMain Input { get; set; }
        public vmCompo_CartWidgetMain Data { get; set; }
    }
}
