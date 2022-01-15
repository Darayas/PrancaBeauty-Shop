using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrancaBeauty.WebApp.Pages.User.Products.Sellers
{
    [Authorize(Roles = Roles.CanEditProductSeller)]
    public class EditModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IMapper _Mapper;
        private readonly IProductSellersApplication _ProductSellersApplication;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;
        public EditModel(IMsgBox msgBox, IServiceProvider serviceProvider, IProductSellersApplication productSellersApplication, IProductVariantItemsApplication productVariantItemsApplication, IMapper mapper, ILocalizer localizer)
        {
            _MsgBox = msgBox;
            _ServiceProvider = serviceProvider;
            _ProductSellersApplication = productSellersApplication;
            _ProductVariantItemsApplication = productVariantItemsApplication;

            Input = new viEditProductSeller();
            _Mapper = mapper;
            _Localizer = localizer;
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

                this.Input.ProductSellerId = Input.ProductSellerId;
                this.Input.UserId = await _ProductSellersApplication.GetUserIdByProductSellerIdAsync(new InpGetUserIdByProductSellerId { ProductSellerId = Input.ProductSellerId });
                this.Input.ProductId = Input.ProductId;

                ViewData["ReturnUrl"] = Input.ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Product/Sellers/List/{Input.ProductId}";

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validations
                if (!ModelState.IsValid)
                    return _MsgBox.ModelStateMsg(ModelState.GetErrors());
                #endregion

                string _UserId = await _ProductSellersApplication.GetUserIdByProductSellerIdAsync(new InpGetUserIdByProductSellerId { ProductSellerId = Input.ProductSellerId });
                if (_UserId == null)
                    return _MsgBox.ModelStateMsg("IdNotFound");

                string SellerId = await _ProductSellersApplication.GetSellerIdAsync(new InpGetSellerId { ProductId = Input.ProductId.ToString(), UserId = _UserId });
                if (SellerId == null)
                    return _MsgBox.ModelStateMsg("IdNotFound");

                var _MappingData = _Mapper.Map<InpEditProductVariants>(Input);
                _MappingData.SellerId = SellerId;

                var _Result = await _ProductVariantItemsApplication.EditProductVariantsAsync(_MappingData);

                if (_Result.IsSucceeded)
                {
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "GotoBack()");
                }
                else
                {
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
                }
            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [BindProperty]
        public viEditProductSeller Input { get; set; }
    }
}
