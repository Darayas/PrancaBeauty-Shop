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

                var qData = await _CartRepository.Get
                                        .Where(a => a.UserId==Input.UserId.ToGuid())
                                        .Select(a => new OutGetItemsInCartItems
                                        {
                                            
                                        })
                                        .ToListAsync();

                var Summary = new OutGetItemsInCart();
                Summary.CountInCart=qData.Count();
                Summary.CurrencySymbol=qData.FirstOrDefault().CurrencySymbol;
                Summary.TaxAmount=0;
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
