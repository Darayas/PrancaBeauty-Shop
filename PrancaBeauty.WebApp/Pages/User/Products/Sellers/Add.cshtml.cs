using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
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
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductApplication _ProductApplication;
        private readonly IProductSellersApplication _ProductSellersApplication;
        public AddModel(IMsgBox msgBox, IProductApplication productApplication, IServiceProvider serviceProvider, IProductSellersApplication productSellersApplication, IMapper mapper, ILocalizer localizer)
        {
            _MsgBox = msgBox;
            _ProductApplication = productApplication;
            _ServiceProvider = serviceProvider;
            Input = new viAddProductSeller();
            _ProductSellersApplication = productSellersApplication;
            _Mapper = mapper;
            _Localizer = localizer;
        }

        public IActionResult OnGet(viGetAddProductSeller Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            ViewData["ProductId"] = Input.ProductId;
            ViewData["VariantId"] = Input.VariantId;
            ViewData["ReturnUrl"] = Input.ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Product/Sellers/List/{Input.ProductId}";

            this.Input.UserId = User.GetUserDetails().UserId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var _MappedData = _Mapper.Map<InpAddSellerWithVariantsToProdcut>(Input);
            if (!User.IsInRole(Roles.CanAddProductSellerAllUser))
                _MappedData.UserId = User.GetUserDetails().UserId;

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
