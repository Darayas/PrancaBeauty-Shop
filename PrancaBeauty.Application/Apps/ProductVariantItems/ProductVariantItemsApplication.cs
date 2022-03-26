﻿using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Contracts.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantItemsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariantItems
{
    public class ProductVariantItemsApplication : IProductVariantItemsApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductVariantItemsRepository _ProductVariantItemsRepository;

        public ProductVariantItemsApplication(IProductVariantItemsRepository productVariantItemsRepository, ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _ProductVariantItemsRepository = productVariantItemsRepository;
            _Logger = logger;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<OperationResult> AddVariantsToProductAsync(InpAddVariantsToProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                if (Input.Variants is null)
                    throw new ArgumentInvalidException($"Variants cant be null.");

                if (Input.Variants.Count() == 0)
                    throw new ArgumentInvalidException($"Variants count must be greater than zero.");

                #endregion

                bool FirstIsMainVariant = true;
                if (await _ProductVariantItemsRepository.Get.Where(a => a.ProductId == Input.ProductId.ToGuid()).AnyAsync(a => a.IsMain))
                    FirstIsMainVariant = false;

                foreach (var item in Input.Variants)
                {
                    #region برسی تکراری نبودن نوع
                    {
                        if (await CheckDuplicateForUserAsync(new InpCheckDuplicateForUser
                        {
                            ProductId = Input.ProductId,
                            ProductSellerId = Input.ProductSellerId,
                            VariantTitle = item.Title,
                            ProductVariantCode = item.ProductCode,
                            VariantValue = item.Value

                        }))
                        {
                            return new OperationResult().Failed(_Localizer["VariantIsDuplicated", item.ProductCode, item.Title, item.Value]);
                        }
                    }
                    #endregion

                    #region برسی VariantId
                    {
                        string VariantId = await GetProductVariantIdAsync(new InpGetProductVariantId { ProductId = Input.ProductId });
                        if (VariantId == null)
                            return new OperationResult().Failed("Error500");

                        if (VariantId != "")
                            Input.VariantId = VariantId;
                    }
                    #endregion

                    var tVariantItem = new tblProductVariantItems()
                    {
                        Id = new Guid().SequentialGuid(),
                        ProductVariantId = Guid.Parse(Input.VariantId),
                        ProductId = Guid.Parse(Input.ProductId),
                        ProductSellerId = Guid.Parse(Input.ProductSellerId),
                        GuaranteeId = item.GuaranteeId != null ? Guid.Parse(item.GuaranteeId) : null,
                        Title = item.Title,
                        Value = item.Value,
                        ProductCode = item.ProductCode,
                        CountInStock = item.CountInStock,
                        IsEnable = item.IsEnable,
                        Percent = double.Parse(item.Percent, new CultureInfo("en-US")),
                        SendBy = item.SendBy,
                        SendFrom = item.SendFrom,
                        IsMain = FirstIsMainVariant
                    };

                    await _ProductVariantItemsRepository.AddAsync(tVariantItem, default, false);

                    FirstIsMainVariant = false;
                }

                await _ProductVariantItemsRepository.SaveChangeAsync();
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

        public async Task<OperationResult> RemoveAllVariantsFromProductAsync(InpRemoveVariantsFromProduct Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductVariantItemsRepository.Get
                                                                .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                                .Select(a => a.Id.ToString())
                                                                .ToArrayAsync();



                await RemoveRangeByVariantItemIdAsync(Input.ProductId, qData);

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

        private async Task<OperationResult> RemoveRangeByVariantItemIdAsync(string ProductId, string[] VariantsItemId)
        {
            try
            {
                var qData = await _ProductVariantItemsRepository.Get
                                                                .Where(a => a.ProductId == Guid.Parse(ProductId))
                                                                .Where(a => VariantsItemId.Contains(a.Id.ToString()))
                                                                .ToListAsync();

                foreach (var item in qData)
                {
                    if (item.IsMain == false)
                    {
                        var _Result = await CheckHasPurchaseForVariantAsync(new InpCheckHasPurchaseForVariant { VariantItemId = item.Id.ToString() });
                        if (_Result.HasValue == false)
                            return new OperationResult().Failed("Error500");

                        if (_Result.Value == false)
                            await _ProductVariantItemsRepository.DeleteAsync(item, default, false);
                    }
                }

                await _ProductVariantItemsRepository.SaveChangeAsync();

                return new OperationResult().Succeeded();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<List<OutGetAllVariantsByProductId>> GetAllVariantsByProductIdAsync(InpGetAllVariantsByProductId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductVariantItemsRepository.Get
                                                               .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                               .Where(a => a.tblProductSellers.SellerId == Guid.Parse(Input.SellerId))
                                                               .Select(a => new OutGetAllVariantsByProductId
                                                               {
                                                                   Id = a.Id.ToString(),
                                                                   VariantId = a.tblProductVariants.Id.ToString(),
                                                                   GuaranteeId = a.GuaranteeId.ToString(),
                                                                   GuaranteeTitle = a.tblGuarantee.tblGuarantee_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(a => a.Title).Single(),
                                                                   Title = a.Title,
                                                                   Value = a.Value,
                                                                   ProductCode = a.ProductCode,
                                                                   CountInStock = a.CountInStock,
                                                                   Percent = a.Percent,
                                                                   SendBy = a.SendBy,
                                                                   SendFrom = a.SendFrom,
                                                                   IsEnable = a.IsEnable,
                                                                   IsConfirm = a.IsConfirm
                                                               })
                                                               .ToListAsync();

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

        public async Task<bool?> CheckHasPurchaseForVariantAsync(InpCheckHasPurchaseForVariant Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                // TODO برسی خرید ثبت شده برای تنوع جاری

                return false;
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

        public async Task<OperationResult> EditProductVariantsAsync(InpEditProductVariants Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region حذف تنوع
                {
                    var qDataToDelete = Input.Variants.Where(a => a.IsDelete)
                                                      .Where(a => a.Id != null)
                                                      .Select(a => a.Id)
                                                      .ToArray();

                    var _Result = await RemoveRangeByVariantItemIdAsync(Input.ProductId, qDataToDelete);
                    if (_Result.IsSucceeded == false)
                        return new OperationResult().Failed(_Result.Message);
                }
                #endregion

                #region افزودن تنوع جدید
                {
                    var qDataToAdd = Input.Variants.Where(a => a.IsDelete == false)
                                                   .Where(a => a.Id == null)
                                                   .ToList();

                    var _Result = await AddVariantsToProductAsync(new InpAddVariantsToProduct
                    {
                        ProductId = Input.ProductId,
                        ProductSellerId = Input.ProductSellerId,
                        VariantId = Input.VariantId,
                        Variants = qDataToAdd.Select(a => new InpAddVariantsToProduct_Variants
                        {
                            GuaranteeId = a.GuaranteeId,
                            ProductCode = a.ProductCode,
                            Title = a.Title,
                            Value = a.Value,
                            CountInStock = a.CountInStock,
                            IsEnable = a.IsEnable,
                            Percent = a.Percent,
                            SendBy = a.SendBy,
                            SendFrom = a.SendFrom
                        }).ToList()
                    });
                    if (_Result.IsSucceeded == false)
                        return new OperationResult().Failed(_Result.Message);
                }
                #endregion

                #region ویرایش تنوع 
                {
                    var qDataToEdit = Input.Variants.Where(a => a.IsDelete == false)
                                                    .Where(a => a.Id != null)
                                                    .ToList();

                    foreach (var item in qDataToEdit)
                    {
                        var qData = await _ProductVariantItemsRepository.Get.Where(a => a.Id == Guid.Parse(item.Id)).SingleOrDefaultAsync();
                        if (qData != null)
                        {
                            qData.Title = item.Title;
                            qData.Value = item.Value;
                            qData.CountInStock = item.CountInStock;
                            qData.GuaranteeId = item.GuaranteeId != null ? Guid.Parse(item.GuaranteeId) : null;
                            qData.IsEnable = item.IsEnable;
                            qData.Percent = double.Parse(item.Percent, new CultureInfo("en-US"));
                            qData.ProductCode = item.ProductCode;
                            qData.ProductVariantId = Guid.Parse(Input.VariantId);
                            qData.SendBy = item.SendBy;
                            qData.SendFrom = item.SendFrom;

                            await _ProductVariantItemsRepository.UpdateAsync(qData, default, false);
                        }
                    }

                    await _ProductVariantItemsRepository.SaveChangeAsync();
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

        private async Task<bool> CheckDuplicateForUserAsync(InpCheckDuplicateForUser Input)
        {
            var IsDuplicate = await _ProductVariantItemsRepository.Get
                                                                  .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                                  .Where(a => a.ProductSellerId == Guid.Parse(Input.ProductSellerId))
                                                                  .Where(a => a.ProductCode == Input.ProductVariantCode || a.Title == Input.VariantTitle || a.Value == Input.VariantValue)
                                                                  .AnyAsync();

            return IsDuplicate;
        }

        public async Task<string> GetProductVariantIdAsync(InpGetProductVariantId Input)
        {
            try
            {
                #region Validatons
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductVariantItemsRepository.Get
                                                    .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                    .Select(a => a.ProductVariantId.ToString())
                                                    .FirstOrDefaultAsync();

                if (qData == null)
                    return "";

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

        public async Task<OperationResult> ChangeStatusVariantItemAsync(InpChangeStatusVariantItem Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductVariantItemsRepository.Get
                                                                .Where(a => a.Id == Guid.Parse(Input.VariantItemId))
                                                                .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                                .Where(a => a.ProductSellerId == Guid.Parse(Input.ProductSellerId))
                                                                .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("IdNotFound");

                if (qData.IsConfirm)
                {
                    // TODO ثبت دلیل رد شدن
                    qData.IsConfirm = false;
                }
                else
                {
                    // TODO ثبت اطلاع رسانی
                    qData.IsConfirm = true;
                }

                await _ProductVariantItemsRepository.UpdateAsync(qData, default, true);

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

        public async Task<OutGetProductPriceByVariantItemId> GetProductPriceByVariantItemIdAsync(InpGetProductPriceByVariantItemId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductVariantItemsRepository.Get
                                                                .Where(a => a.Id == Input.ProductVariantItemId.ToGuid())
                                                                .Select(a => new
                                                                {
                                                                    MainPrice = a.tblProducts.tblProductPrices.Where(b => b.IsActive).Select(b => b.Price).Single(),
                                                                    CurrencySymbol = a.tblProducts.tblProductPrices.Where(b => b.IsActive).Select(b => b.tblCurrency.Symbol).Single(),
                                                                    SellerPercentPrice = a.Percent,
                                                                    SavePercentPrice = 0, // TODO: For Discount
                                                                })
                                                                .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                double _OldProductPrice = qData.MainPrice + ((qData.MainPrice / 100) * qData.SellerPercentPrice);
                double _ProductPrice = _OldProductPrice + ((_OldProductPrice / 100) * qData.SavePercentPrice);
                if (qData.SavePercentPrice == 0)
                    _OldProductPrice = 0;

                return new OutGetProductPriceByVariantItemId { ProductPrice = _ProductPrice, ProductOldPrice = _OldProductPrice, CurrencySymbol = qData.CurrencySymbol };
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
