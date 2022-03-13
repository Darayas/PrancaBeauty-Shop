using PrancaBeauty.Application.Contracts.ProductReviewsAttributeValues;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsAttributeValues
{
    public interface IProductReviewsAttributeValuesApplication
    {
        Task<OperationResult> AddAttributesToReviewAsync(InpAddAttributesToReview Input);
        Task<List<OutGetAvgAttributesByReviewId>> GetAvgAttributesByReviewIdAsync(InpGetAvgAttributesByReviewId Input);
    }
}