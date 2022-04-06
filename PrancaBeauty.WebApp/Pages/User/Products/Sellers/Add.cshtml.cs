using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Apps.Seller;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductSellers;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sellers;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.Products.Sellers
{
    [Authorize(Roles = Roles.CanAddProductSeller)]
    public class AddModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductApplication _ProductApplication;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;
        private readonly IProductSellersApplication _ProductSellersApplication;
        private readonly ISellerApplication _SellerApplication;
        public AddModel(IMsgBox msgBox, IProductApplication productApplication, IServiceProvider serviceProvider, IProductSellersApplication productSellersApplication, IMapper mapper, ILocalizer localizer, IProductVariantItemsApplication productVariantItemsApplication, ISellerApplication sellerApplication)
        {
            _MsgBox = msgBox;
            _ProductApplication = productApplication;
            _ServiceProvider = serviceProvider;
            _ProductSellersApplication = productSellersApplication;
            _Mapper = mapper;
            _Localizer = localizer;
            _ProductVariantItemsApplication = productVariantItemsApplication;

            Input = new viAddProductSeller();
            _SellerApplication = sellerApplication;
        }

        public async Task<IActionResult> OnGetAsync(viGetAddProductSeller Input, string LangId)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            #region CheckVariantId
            {
                string _VariantId = await _ProductVariantItemsApplication.GetProductVariantIdAsync(new InpGetProductVariantId { ProductId = Input.ProductId });
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
            ViewData["LangId"] = LangId;

            this.Input.SellerId = await _SellerApplication.GetSellerIdByUserIdAsync(new InpGetSellerIdByUserId { UserId = User.GetUserDetails().UserId });
            this.Input.ProductId = Input.ProductId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var _MappedData = _Mapper.Map<InpAddSellerWithVariantsToProdcut>(Input);
            if (!User.IsInRole(Roles.CanAddProductSellerAllUser))
                _MappedData.SellerId = await _SellerApplication.GetSellerIdByUserIdAsync(new InpGetSellerIdByUserId { UserId = User.GetUserDetails().UserId });

            var _Result = await _ProductSellersApplication.AddSellerWithVariantsToProdcutAsync(_MappedData);
            if (_Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "GotoBack()");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
            }
        }

        [BindProperty]
        public viAddProductSeller Input { get; set; }

    }
}
