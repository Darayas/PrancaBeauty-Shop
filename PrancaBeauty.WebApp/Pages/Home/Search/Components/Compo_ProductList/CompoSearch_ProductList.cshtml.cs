using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Home.Search.Components.Compo_ProductList
{
    public class CompoSearch_ProductListModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductApplication _ProductApplication;

        public CompoSearch_ProductListModel(ILogger logger, IServiceProvider serviceProvider, IProductApplication productApplication)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ProductApplication=productApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompoSearch_ProductList Input { get; set; }
        public vmCompoSearch_ProductList Data { get; set; }
    }
}
