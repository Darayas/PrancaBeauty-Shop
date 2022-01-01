using Framework.Common.ExMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.Products.Sellers
{
    [Authorize(Roles = Roles.CanAddProductSeller)]
    public class AddModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductApplication _ProductApplication;
        public AddModel(IMsgBox msgBox, IProductApplication productApplication, IServiceProvider serviceProvider)
        {
            _MsgBox = msgBox;
            _ProductApplication = productApplication;
            _ServiceProvider = serviceProvider;
        }

        public IActionResult OnGet(viGetAddProductSeller Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            ViewData["ProductId"]=Input.ProductId;
            ViewData["ReturnUrl"] = Input.ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Product/Sellers/List/{Input.ProductId}";

            return Page();
        }

        [BindProperty]
        public viAddProductSeller Input { get; set; }

    }
}
