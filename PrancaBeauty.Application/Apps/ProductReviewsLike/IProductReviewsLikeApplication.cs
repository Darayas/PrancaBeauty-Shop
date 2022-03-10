using PrancaBeauty.Application.Contracts.ProductReviewLikes;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsLike
{
    public interface IProductReviewsLikeApplication
    {
        Task<int> LikeReviewAsync(InpLikeReview Input);
    }
}