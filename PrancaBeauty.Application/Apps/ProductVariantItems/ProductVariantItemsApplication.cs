using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Results;
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

                if (Input.Variants == null)
                    throw new ArgumentInvalidException($"{nameof(Input.Variants)} cant be null.");

                #endregion

                foreach (var item in Input.Variants)
                {
                    var tVariantItem = new tblProductVariantItems()
                    {
                        Id = new Guid().SequentialGuid(),
                        ProductVariantId = Guid.Parse(Input.VariantId),
                        ProductId = Guid.Parse(Input.ProductId),
                        ProductSellerId = Guid.Parse(Input.SellerId),
                        GuaranteeId = item.GuaranteeId != null ? Guid.Parse(item.GuaranteeId) : null,
                        Title = item.Title,
                        Value = item.Value,
                        ProductCode = item.ProductCode,
                        CountInStock = item.CountInStock,
                        IsEnable = item.IsEnable,
                        Percent = double.Parse(item.Percent, new CultureInfo("en-US")),
                        SendBy = item.SendBy,
                        SendFrom = item.SendFrom
                    };

                    await _ProductVariantItemsRepository.AddAsync(tVariantItem, default, false);
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

                var qData = await _ProductVariantItemsRepository.Get.Where(a => a.ProductId == Guid.Parse(Input.ProductId)).ToListAsync();

                await _ProductVariantItemsRepository.DeleteRangeAsync(qData, default, false);

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

        public async Task<List<OutGetAllVariantsByProductId>> GetAllVariantsByProductIdAsync(InpGetAllVariantsByProductId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductVariantItemsRepository.Get
                                                               .Where(a => a.ProductId == Guid.Parse(Input.ProductId))
                                                               .Select(a => new OutGetAllVariantsByProductId
                                                               {
                                                                   Id = a.Id.ToString(),
                                                                   VariantId = a.tblProductVariants.Id.ToString(),
                                                                   GuaranteeId = a.GuaranteeId.ToString(),
                                                                   Title = a.Title,
                                                                   Value = a.Value,
                                                                   ProductCode = a.ProductCode,
                                                                   CountInStock = a.CountInStock,
                                                                   Percent = a.Percent,
                                                                   SendBy = a.SendBy,
                                                                   SendFrom = a.SendFrom,
                                                                   IsEnable = a.IsEnable
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
    }
}
