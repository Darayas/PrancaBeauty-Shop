using Framework.Common.ExMethods;
using Framework.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ProductPropertiesValues;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.Products.Sellers
{
    [Authorize(Roles = Roles.CanEditProductSeller)]
    public class EditModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductSellersApplication _ProductSellersApplication;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;
        public EditModel(IMsgBox msgBox, IServiceProvider serviceProvider, IProductSellersApplication productSellersApplication, IProductVariantItemsApplication productVariantItemsApplication)
        {
            _MsgBox = msgBox;
            _ServiceProvider = serviceProvider;
            _ProductSellersApplication = productSellersApplication;
            _ProductVariantItemsApplication = productVariantItemsApplication;

            Input = new viEditProductSeller();
        }

        public async Task<IActionResult> OnGetAsync(viGetEditProductSeller Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region CheckVariantId
                {
                    string _VariantId = await _ProductVariantItemsApplication.GetProductVariantAsync(new InpGetProductVariant { ProductId = Input.ProductId });
                    if (_VariantId == "")
                    {
                        ViewData["ProductVariantEnable"] = true;
                        ViewData["VariantId"] = "";
                    }
                    else
                    {
                        ViewData["ProductVariantEnable"] = false;
                        ViewData["VariantId"] = _VariantId;
                    }
                }
                #endregion


                ViewData["ReturnUrl"] = Input.ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Product/Sellers/List/{Input.ProductId}";

                this.Input.UserId = User.GetUserDetails().UserId;
                this.Input.ProductId = Input.ProductId;

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message.Replace(",", "<br/>"));
            }
        }

        [BindProperty]
        public viEditProductSeller Input { get; set; }
    }
}
