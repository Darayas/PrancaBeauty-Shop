using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ProductReviewsAttributeValues;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeValuesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeValuesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsAttributeValues
{
    public class ProductReviewsAttributeValuesApplication : IProductReviewsAttributeValuesApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductReviewsAttributeValuesRepository _ProductReviewsAttributeValuesRepository;
        private readonly IProductReviewsAttributeRepository _IProductReviewsAttributeRepository;

        public ProductReviewsAttributeValuesApplication(IProductReviewsAttributeValuesRepository productReviewsAttributeValuesRepository, ILogger logger, IServiceProvider serviceProvider, IProductReviewsAttributeRepository iProductReviewsAttributeRepository)
        {
            _ProductReviewsAttributeValuesRepository = productReviewsAttributeValuesRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _IProductReviewsAttributeRepository = iProductReviewsAttributeRepository;
        }

        public async Task<OperationResult> AddAttributesToReviewAsync(InpAddAttributesToReview Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                foreach (var item in Input.Items)
                {
                    var tProductReviewAttrVal = new tblProductReviewsAttributeValues
                    {
                        Id = new Guid().SequentialGuid(),
                        ProductReviewAttributeId = Guid.Parse(item.AttributeId),
                        ProductReviewId = Guid.Parse(Input.ProductReviewId),
                        Value = item.Value
                    };

                    await _ProductReviewsAttributeValuesRepository.AddAsync(tProductReviewAttrVal, default, false);
                }

                await _ProductReviewsAttributeValuesRepository.SaveChangeAsync();

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

        public async Task<List<OutGetAvgAttributesByReviewId>> GetAvgAttributesByReviewIdAsync(InpGetAvgAttributesByReviewId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _IProductReviewsAttributeRepository.Get
                                                                     .Where(a => a.tblProductReviewsAttributeValues.Where(b => b.tblProductReviews.ProductId == Input.ProductId.ToGuid() && b.Value > 0 && b.tblProductReviews.IsConfirm).Any())
                                                                     .Select(a => new OutGetAvgAttributesByReviewId
                                                                     {
                                                                         Title = a.tblProductReviewsAttribute_Translate.Where(b => b.LangId == Input.LangId.ToGuid()).Select(a => a.Title).Single(),
                                                                         Avg = a.tblProductReviewsAttributeValues.Count(a => a.tblProductReviews.IsConfirm) > 0 ? a.tblProductReviewsAttributeValues.Where(a => a.tblProductReviews.IsConfirm).Average(b => b.Value) : 0
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
