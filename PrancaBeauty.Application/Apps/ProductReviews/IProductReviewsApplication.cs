using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ProdcutReviews;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviews
{
    public interface IProductReviewsApplication
    {
        Task<OperationResult> AddReviewFromUserAsync(InpAddReviewFromUser Input);
        Task<OperationResult> ChanageStatusReviewAsync(InpChanageStatusReview Input);
        Task<(OutPagingData PageingData, OutGetReviewsForProductDetails RevivewsData)> GetReviewsForProductDetailsAsync(InpGetReviewsForProductDetails Input);
        Task<OperationResult> RemoveProductReviewAsync(InpRemoveProductReview Input);
    }
}