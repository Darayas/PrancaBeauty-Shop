using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Apps.Seller;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductSellers;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sellers;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.Products.Sellers
{
    [Authorize(Roles = Roles.CanViewDetailsProductSeller)]
    public class DetailsModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISellerApplication _SellersApplication;
        private readonly IProductApplication _ProductApplication;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;
        private readonly IProductSellersApplication _ProductSellersApplication;
        public DetailsModel(IMsgBox msgBox, IServiceProvider serviceProvider, IProductSellersApplication productSellersApplication, ISellerApplication sellersApplication, ILocalizer localizer, IMapper mapper, IProductApplication productApplication, IProductVariantItemsApplication productVariantItemsApplication)
        {
            _MsgBox = msgBox;
            _ServiceProvider = serviceProvider;
            _ProductSellersApplication = productSellersApplication;
            _SellersApplication = sellersApplication;
            _Localizer = localizer;
            _Mapper = mapper;
            _ProductApplication = productApplication;
            _ProductVariantItemsApplication = productVariantItemsApplication;
        }

        public async Task<IActionResult> OnGetAsync(viGetProductSellerDetails Input, string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get sellerId by ProductSellerId
                string _SellerId;
                {
                    _SellerId = await _ProductSellersApplication.GetSellerIdByProductSellerIdAsync(new InpGetSellerIdByProductSellerId { ProductSellerId = Input.ProductSellerId });
                    if (_SellerId == null)
                        throw new ArgumentInvalidException("ProductSellerId is invalid");
                }
                #endregion

                #region Get Seller summary
                OutGetSummaryBySellerId qSallerSummay;
                {
                    qSallerSummay = await _SellersApplication.GetSummaryBySellerIdAsync(new InpGetSummaryBySellerId { SellerId = _SellerId, LangId = LangId });
                    Data = _Mapper.Map<vmProductSellerDetails>(qSallerSummay);
                }
                #endregion

                #region Get product summary
                {
                    var qSummary = await _ProductApplication.GetSummaryByIdAsync(new InpGetSummaryById { ProductId = Input.ProductId });
                    if (qSummary == null)
                        throw new ArgumentInvalidException("ProductId is invalid");

                    Data.ProductName = qSummary.Name;
                    Data.ProductTitle = qSummary.Title;
                }
                #endregion

                ProductId = Input.ProductId;
                ProductSellerId = Input.ProductSellerId;
                SellerId = _SellerId;

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostReadAsync([DataSourceRequest] DataSourceRequest request, viGetProductSellerVariants Input, string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _ProductVariantItemsApplication.GetAllVariantsByProductIdAsync(new InpGetAllVariantsByProductId { ProductId = Input.ProductId, SellerId = Input.SellerId, LangId = LangId });
                if (_Result == null)
                    throw new ArgumentInvalidException("NoVariantsFound");

                var qData = _Mapper.Map<List<vmGetProductSellerVariants>>(_Result);

                return new JsonResult(await qData.ToDataSourceResultAsync(request));

            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> OnPostChangeStatusAsync(viChangeStatusVariantItemId Input)
        {
            if (!User.IsInRole(Roles.CanChangeStatusProductSeller))
                return _MsgBox.AccessDeniedMsg();

            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var _Result = await _ProductVariantItemsApplication.ChangeStatusVariantItemAsync(new InpChangeStatusVariantItem
            {
                ProductId = Input.ProductId,
                ProductSellerId = Input.ProductSellerId,
                VariantItemId = Input.VariantItemId
            });
            if (_Result.IsSucceeded)
                return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "RefreshData()");
            else
                return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
        }

        public vmProductSellerDetails Data { get; set; }
        public string ProductId { get; set; }
        public string ProductSellerId { get; set; }
        public string SellerId { get; set; }
    }
}
