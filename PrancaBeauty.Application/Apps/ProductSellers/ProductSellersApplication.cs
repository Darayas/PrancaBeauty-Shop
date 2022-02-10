using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Apps.Seller;
using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Contracts.Sellers;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductSellers
{
    public class ProductSellersApplication : IProductSellersApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductSellersRepsoitory _ProductSellersRepsoitory;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;
        private readonly ISellerApplication _SellerApplication;
        public ProductSellersApplication(IProductSellersRepsoitory productSellersRepsoitory, ILogger logger, IServiceProvider serviceProvider, IProductVariantItemsApplication productVariantItemsApplication, ISellerApplication sellerApplication)
        {
            _ProductSellersRepsoitory = productSellersRepsoitory;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _ProductVariantItemsApplication = productVariantItemsApplication;
            _SellerApplication = sellerApplication;
        }

        public async Task<OperationResult> AddSellerToProdcutAsync(InpAddSellerToProdcut Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var tProductSeller = new tblProductSellers
                {
                    Id = new Guid().SequentialGuid(),
                    SellerId = Guid.Parse(Input.SellerId),
                    ProductId = Guid.Parse(Input.ProductId),
                    IsConfirm = Input.IsConfirm,
                    Date = DateTime.Now
                };

                await _ProductSellersRepsoitory.AddAsync(tProductSeller, default, true);

                return new OperationResult().Succeeded(1, tProductSeller.Id.ToString());
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

        public async Task<OperationResult> AddSellerWithVariantsToProdcutAsync(InpAddSellerWithVariantsToProdcut Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                string _ProductSellerId = null;

                #region برسی کاربر جاری که فروشنده ی محصول نباشد
                {
                    _ProductSellerId = await GetProductSellerIdAsync(new InpGetProductSellerId { ProductId = Input.ProductId, SellerId = Input.SellerId });
                    if (_ProductSellerId == null)
                    {
                        var _Result = await AddSellerToProdcutAsync(new InpAddSellerToProdcut
                        {
                            SellerId = Input.SellerId,
                            ProductId = Input.ProductId,
                            IsConfirm = false
                        });
                        if (_Result.IsSucceeded)
                            _ProductSellerId = _Result.Message;
                    }
                }
                #endregion

                #region افزودن تنوع محصولات برای فروشنده
                {
                    var _Result = await _ProductVariantItemsApplication.AddVariantsToProductAsync(new InpAddVariantsToProduct
                    {
                        ProductId = Input.ProductId,
                        ProductSellerId = _ProductSellerId,
                        VariantId = Input.VariantId,
                        Variants = Input.Variants.Select(a => new InpAddVariantsToProduct_Variants
                        {
                            CountInStock = a.CountInStock,
                            GuaranteeId = a.GuaranteeId,
                            IsEnable = a.IsEnable,
                            Percent = a.Percent,
                            ProductCode = a.ProductCode,
                            SendBy = a.SendBy,
                            SendFrom = a.SendFrom,
                            Title = a.Title,
                            Value = a.Value,
                        }).ToList()
                    });

                    if (!_Result.IsSucceeded)
                        return new OperationResult().Failed(_Result.Message);
                }
                #endregion

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> RemoveProductSellerAsync(InpRemoveProductSeller Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region حذف تنوع
                {
                    var _Result = await _ProductVariantItemsApplication.RemoveAllVariantsFromProductAsync(new InpRemoveVariantsFromProduct
                    {
                        ProductId = Input.ProductId
                    });
                    if (!_Result.IsSucceeded)
                    {
                        return new OperationResult().Failed(_Result.Message);
                    }
                }
                #endregion

                #region حذف فروشنده از محصول
                {
                    var qProductSeller = await _ProductSellersRepsoitory.Get
                                                                        .Where(a => a.Id == Guid.Parse(Input.ProductSellerId))
                                                                        .SingleOrDefaultAsync();
                    if (qProductSeller == null)
                        return new OperationResult().Failed("ProductSellerIdNotFound");

                    await _ProductSellersRepsoitory.DeleteAsync(qProductSeller, default, true);

                }
                #endregion

                return new OperationResult().Succeeded();
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

        public async Task<OperationResult> ChangeStatusProductSellerAsync(InpChangeStatusProductSeller Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region تغییر وضعیت فروشنده ی محصول
                {
                    var qProductSeller = await _ProductSellersRepsoitory.Get
                                                                        .Where(a => a.Id == Guid.Parse(Input.ProductSellerId))
                                                                        .SingleOrDefaultAsync();
                    if (qProductSeller == null)
                        return new OperationResult().Failed("ProductSellerIdNotFound");

                    if (qProductSeller.IsConfirm)
                    {
                        // TODO ثبت دلیل رد شدن
                        qProductSeller.IsConfirm = false;
                    }
                    else
                    {
                        // TODO ارسال اطلاع رسانی
                        qProductSeller.IsConfirm = true;
                    }

                    await _ProductSellersRepsoitory.UpdateAsync(qProductSeller, default, true);

                }
                #endregion

                return new OperationResult().Succeeded();
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

        public async Task<string> GetProductSellerIdAsync(InpGetProductSellerId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductSellersRepsoitory.Get
                                                           .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                           .Where(a => a.tblSellers.Id == Guid.Parse(Input.SellerId))
                                                           .Select(a => a.Id.ToString())
                                                           .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<(OutPagingData, List<vmGetAllSellerForManageByProductId>)> GetAllSellerForManageByProductIdAsync(InpGetAllSellerForManageByProductId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _ProductSellersRepsoitory.Get
                                                     .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                     .Where(a => Input.UserId != null ? a.tblSellers.UserId == Guid.Parse(Input.UserId) : true)
                                                     .Select(a => new vmGetAllSellerForManageByProductId
                                                     {
                                                         Id = a.Id.ToString(),
                                                         FullName = a.tblSellers.tblUsers.FirstName + " " + a.tblSellers.tblUsers.LastName,
                                                         SellerName = a.tblSellers.tblSeller_Translates.Where(a => a.LangId == Guid.Parse(Input.LangId)).Select(a => a.Title).Single(),
                                                         Date = a.Date,
                                                         IsConfirm = a.IsConfirm,
                                                         HasUnConfermVariants = !a.tblProductVariantItems.Any(b => b.IsConfirm == false)
                                                     });

                #region شرط
                {
                    if (Input.FullName != null)
                        qData = qData.Where(a => a.FullName.Contains(Input.FullName));
                }
                #endregion

                #region مرتب سازی
                {
                    qData = qData.OrderBy(a => a.Date).OrderByDescending(a => a.IsConfirm);
                }
                #endregion

                #region صفحه بندی
                var _PagingData = PagingData.Calc(await qData.LongCountAsync(), Input.Page, Input.Take);
                #endregion

                return (_PagingData, await qData.Skip((int)_PagingData.Skip).Take(_PagingData.Take).ToListAsync());
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

        public async Task<string> GetUserIdByProductSellerIdAsync(InpGetUserIdByProductSellerId Input)

        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductSellersRepsoitory.Get
                                                    .Where(a => a.Id == Guid.Parse(Input.ProductSellerId))
                                                    .Select(a => a.tblSellers.UserId.ToString())
                                                    .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<string> GetSellerIdByProductSellerIdAsync(InpGetSellerIdByProductSellerId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductSellersRepsoitory.Get
                                                    .Where(a => a.Id == Guid.Parse(Input.ProductSellerId))
                                                    .Select(a => a.SellerId.ToString())
                                                    .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

    }
}
