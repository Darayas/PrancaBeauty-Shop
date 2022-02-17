using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductReviews
{
    [Authorize]
    public class Compo_AddProductReviewsModel : PageModel
    {
        public Compo_AddProductReviewsModel()
        {

        }

        public IActionResult OnGet(viGetCompo_AddProductReviews Input)
        {
            return Page();
        }

        [BindProperty]
        public viCompo_AddProductReviews Input { get; set; }
    }
}
