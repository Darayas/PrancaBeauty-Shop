using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductSellers;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
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

        public async Task<IActionResult> OnGetAsync(viGetEditProductSeller Input,string LangId)
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
                this.Input.SellerId = await _ProductSellersApplication.GetSellerIdByProductSellerIdAsync(new InpGetSellerIdByProductSellerId { ProductSellerId = Input.ProductSellerId });
                this.Input.ProductId = Input.ProductId;

                ViewData["ReturnUrl"] = Input.ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Product/Sellers/List/{Input.ProductId}";
                ViewData["LangId"] = LangId;
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

                string _SellerId = await _ProductSellersApplication.GetSellerIdByProductSellerIdAsync(new InpGetSellerIdByProductSellerId { ProductSellerId = Input.ProductSellerId });
                if (_SellerId == null)
                    return _MsgBox.ModelStateMsg("IdNotFound");

                string ProductSellerId = await _ProductSellersApplication.GetProductSellerIdAsync(new InpGetProductSellerId { ProductId = Input.ProductId.ToString(), SellerId = _SellerId });
                if (ProductSellerId == null)
                    return _MsgBox.ModelStateMsg("IdNotFound");

                var _MappingData = _Mapper.Map<InpEditProductVariants>(Input);
                _MappingData.ProductSellerId = ProductSellerId;

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
