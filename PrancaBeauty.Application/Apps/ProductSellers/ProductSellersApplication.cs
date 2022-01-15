using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
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
        public ProductSellersApplication(IProductSellersRepsoitory productSellersRepsoitory, ILogger logger, IServiceProvider serviceProvider, IProductVariantItemsApplication productVariantItemsApplication)
        {
            _ProductSellersRepsoitory = productSellersRepsoitory;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _ProductVariantItemsApplication = productVariantItemsApplication;
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
                    UserId = Guid.Parse(Input.UserId),
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

                string _SellerId = null;

                #region برسی کاربر جاری که فروشنده ی محصول نباشد
                {
                    _SellerId = await GetSellerIdAsync(new InpGetSellerId { ProductId = Input.ProductId, UserId = Input.UserId });
                    if (_SellerId == null)
                    {
                        var _Result = await AddSellerToProdcutAsync(new InpAddSellerToProdcut
                        {
                            UserId = Input.UserId,
                            ProductId = Input.ProductId,
                            IsConfirm = false
                        });
                        if (_Result.IsSucceeded)
                            _SellerId = _Result.Message;
                    }
                }
                #endregion

                #region افزودن تنوع محصولات برای فروشنده
                {
                    var _Result = await _ProductVariantItemsApplication.AddVariantsToProductAsync(new InpAddVariantsToProduct
                    {
                        ProductId = Input.ProductId,
                        SellerId = _SellerId,
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

        public async Task<string> GetSellerIdAsync(InpGetSellerId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductSellersRepsoitory.Get
                                                           .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                           .Where(a => a.UserId == Guid.Parse(Input.UserId))
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
                                                     .Where(a => Input.UserId != null ? a.UserId == Guid.Parse(Input.UserId) : true)
                                                     .Select(a => new vmGetAllSellerForManageByProductId
                                                     {
                                                         Id = a.Id.ToString(),
                                                         FullName = a.tblUsers.FirstName + " " + a.tblUsers.LastName,
                                                         Date = a.Date,
                                                         IsConfirm = a.IsConfirm
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
                                                    .Select(a => a.UserId.ToString())
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
