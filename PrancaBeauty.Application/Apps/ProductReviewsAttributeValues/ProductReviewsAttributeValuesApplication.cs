using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.ProductReviewsAttributeValues;
using PrancaBeauty.Application.Contracts.Results;
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

        public ProductReviewsAttributeValuesApplication(IProductReviewsAttributeValuesRepository productReviewsAttributeValuesRepository, ILogger logger, IServiceProvider serviceProvider)
        {
            _ProductReviewsAttributeValuesRepository = productReviewsAttributeValuesRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
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
                        Value = item.Value.ToString()
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

        public async Task<OutGetAvgAttributesByReviewId> GetAvgAttributesByReviewIdAsync(InpGetAvgAttributesByReviewId Input)
        {

        }
    }
}
