using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Cart;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Cart.CartAgg.Contracts;
using PrancaBeauty.Domin.Cart.CartAgg.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Carts
{
    public class CartApplication : ICartApplication
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ICartRepository _CartRepository;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;

        public CartApplication(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, ICartRepository cartRepository, IProductVariantItemsApplication productVariantItemsApplication)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _CartRepository=cartRepository;
            _ProductVariantItemsApplication=productVariantItemsApplication;
        }

        public async Task<OperationResult> AddToCartAsync(InpAddToCart Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region برسی موجود نبودن محصول و نوع جاری در سبد خرید کاربر
                tblCarts tCart = null;
                {
                    tCart = await _CartRepository.Get
                                            .Where(a => a.UserId==Input.UserId.ToGuid())
                                            .Where(a => a.VariantItemId==Input.VariantItemId.ToGuid())
                                            .SingleOrDefaultAsync();
                }
                #endregion

                #region افزودن یک مورد به تعداد در صورتی که محصول از قبل در سبد موجو باشد
                {
                    if (tCart!=null)
                    {
                        #region برسی موجود بودن تعداد در انبار فروشنده
                        {
                            var CheckExist = await _ProductVariantItemsApplication.ExistVariantInStockAsync(new InpExistVariantInStock { VariantItemId=Input.VariantItemId, CountToCheck=(++tCart.Count) });
                            if (CheckExist==null)
                                return new OperationResult().Failed("Error500");

                            if (CheckExist==false)
                                return new OperationResult().Failed("ProductNotExistInStock");
                        }
                        #endregion

                        tCart.Count++;
                        tCart.Date=DateTime.Now;

                        await _CartRepository.UpdateAsync(tCart, default, true);
                    }
                }
                #endregion

                #region افزودن محصول و نوع به سبد خرید کاربر
                {
                    if (tCart == null)
                    {
                        #region برسی موجود بودن تعداد در انبار فروشنده
                        {
                            var CheckExist = await _ProductVariantItemsApplication.ExistVariantInStockAsync(new InpExistVariantInStock { VariantItemId=Input.VariantItemId, CountToCheck=1 });
                            if (CheckExist==null)
                                return new OperationResult().Failed("Error500");

                            if (CheckExist==false)
                                return new OperationResult().Failed("ProductNotExistInStock");
                        }
                        #endregion

                        tCart= new tblCarts()
                        {
                            Id= new Guid().SequentialGuid(),
                            UserId=Input.UserId.ToGuid(),
                            ProductId=Input.ProductId.ToGuid(),
                            SellerId=Input.SellerId.ToGuid(),
                            VariantItemId=Input.VariantItemId.ToGuid(),
                            Date=DateTime.Now,
                            Count=1
                        };

                        await _CartRepository.AddAsync(tCart, default, true);
                    }
                }
                #endregion

                return new OperationResult().Succeeded("AddProductToCartWasSuccessfull");
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OutGetItemsInCart> GetItemsInCartAsync(InpGetItemsInCart Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await (from a in _CartRepository.Get
                                   where a.UserId==Input.UserId.ToGuid()
                                   let CurrencySymbol = a.tblProducts.tblProductPrices.Where(b => b.IsActive && b.CurrencyId==Input.CurrencyId.ToGuid()).Select(b => b.tblCurrency.Symbol).Single()
                                   let Price = a.tblProducts.tblProductPrices.Where(a => a.IsActive).Select(b => b.Price).Single()
                                   let SellerPercent = a.tblProducts.tblProductVariantItems.Where(b => b.IsEnable && b.IsConfirm && b.CountInStock>0).Select(e => new { SellerPercent = e.Percent - (e.tblProductDiscounts!=null ? e.tblProductDiscounts.Percent : 0), Percent = e.Percent }).OrderBy(e => e.SellerPercent).FirstOrDefault().Percent
                                   let PercentSavePrice = a.tblProducts.tblProductVariantItems.Where(b => b.IsEnable && b.IsConfirm && b.CountInStock>0).Select(e => new { SellerPercent = e.Percent - (e.tblProductDiscounts!=null ? e.tblProductDiscounts.Percent : 0), SavePercent = (e.tblProductDiscounts!=null ? e.tblProductDiscounts.Percent : 0) }).OrderBy(e => e.SellerPercent).FirstOrDefault().SavePercent
                                   let OldPrice = Price + ((Price/100)*SellerPercent)
                                   let NewPrice = OldPrice - ((OldPrice/100)*PercentSavePrice)
                                   select new OutGetItemsInCartItems
                                   {
                                       Id=a.Id.ToString(),
                                       ProductName=a.tblProducts.Name,
                                       ProductTitle=a.tblProducts.Title,
                                       TaxPercent=0, // TODO: از جدول گروه مالیاتی خوانده شود
                                       Qty=a.Count,
                                       CurrencySymbol=CurrencySymbol,
                                       ProductImgUrl=a.tblProducts.tblProductMedia.Select(b => b.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                                               + b.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                                               + b.tblFiles.tblFilePaths.Path
                                                                                               + b.tblFiles.FileName).First(),
                                       OldPrice=OldPrice,
                                       Price=NewPrice,
                                       PercentSavePrice=PercentSavePrice
                                   }).ToListAsync();

                var Summary = new OutGetItemsInCart();
                Summary.CountInCart=qData.Count();
                Summary.CurrencySymbol=qData.FirstOrDefault().CurrencySymbol;
                Summary.TaxAmount=qData.Sum(a => ((a.Price/100)* a.TaxPercent) * a.Qty);
                Summary.ShippingAmount=0;
                Summary.Items=qData;
                Summary.TotalAmount=qData.Sum(a => a.Price)
                                        + Summary.TaxAmount
                                        + Summary.ShippingAmount;

                return Summary;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }
    }
}
