using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Apps.Seller;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.Application.Contracts.Sellers;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
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
        private readonly IProductSellersApplication _ProductSellersApplication;
        public DetailsModel(IMsgBox msgBox, IServiceProvider serviceProvider, IProductSellersApplication productSellersApplication, ISellerApplication sellersApplication, ILocalizer localizer, IMapper mapper, IProductApplication productApplication)
        {
            _MsgBox = msgBox;
            _ServiceProvider = serviceProvider;
            _ProductSellersApplication = productSellersApplication;
            _SellersApplication = sellersApplication;
            _Localizer = localizer;
            _Mapper = mapper;
            _ProductApplication = productApplication;
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
                }
                #endregion

                Data = _Mapper.Map<vmProductSellerDetails>(qSallerSummay);

                #region Get product summary
                {
                    var qSummary = await _ProductApplication.GetSummaryByIdAsync(new InpGetSummaryById { ProductId = Input.ProductId });
                    if (qSummary == null)
                        throw new ArgumentInvalidException("ProductId is invalid");

                    Data.ProductName = qSummary.Name;
                    Data.ProductTitle = qSummary.Title;
                }
                #endregion

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public vmProductSellerDetails Data { get; set; }

        public string ProductId { get; set; }
    }
}
