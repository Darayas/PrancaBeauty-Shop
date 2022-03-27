using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductSeller
{
    public class Compo_ProductSellersModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync(viGetCompo_ProductSellers Input)
        {
            return Page();
        }

        public List<vmCompo_ProductSellers> Data { get; set; }
    }
}
