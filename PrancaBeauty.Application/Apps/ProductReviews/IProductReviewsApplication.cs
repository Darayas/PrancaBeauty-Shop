using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ProdcutReviews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviews
{
    public interface IProductReviewsApplication
    {
        Task<(OutPagingData PageingData, List<OutGetReviewsForProductDetails> LstRevivews)> GetReviewsForProductDetailsAsync(InpGetReviewsForProductDetails Input);
    }
}