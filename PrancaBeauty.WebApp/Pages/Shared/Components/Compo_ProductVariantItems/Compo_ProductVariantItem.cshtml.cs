using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.WebApp.Models.ViewInput;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductVariantItems
{
    public class Compo_ProductVariantItemModel : PageModel
    {

        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;

        public Compo_ProductVariantItemModel(ILogger logger, IMapper mapper, IProductVariantItemsApplication ProductVariantItemsApplication)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ProductVariantItemsApplication=ProductVariantItemsApplication;
        }



        public async Task<IActionResult> OnGetAsync()
        {

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ProductVariantItem Input { get; set; }
    }
}
