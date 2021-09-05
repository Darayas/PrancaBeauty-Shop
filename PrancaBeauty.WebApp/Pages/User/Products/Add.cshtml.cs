using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.Products
{
    [Authorize(Roles = Roles.CanAddProduct)]
    public class AddModel : PageModel
    {
        private readonly IProductApplication _ProductApplication;
        public AddModel(IProductApplication productApplication)
        {
            _ProductApplication = productApplication;
            Input = new viAddProduct();
        }

        public IActionResult OnGet(string ReturnUrl)
        {
            Input.ReturnUrl = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Products/List";

            return Page();
        }

        [BindProperty]
        public viAddProduct Input { get; set; }
    }
}
