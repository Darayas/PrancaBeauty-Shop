using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductReviewLikes;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsLike
{
    public interface IProductReviewsLikeApplication
    {
        Task<(int CountDisLike, bool IsDisLike)> DisLikeReviewAsync(InpDisLikeReview Input);
        Task<(int CountLike, bool IsLike)> LikeReviewAsync(InpLikeReview Input);
    }
}