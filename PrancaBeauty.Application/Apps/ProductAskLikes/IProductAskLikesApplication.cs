using PrancaBeauty.Application.Contracts.ProductAskLikes;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductAskLikes
{
    public interface IProductAskLikesApplication
    {
        Task<(int CountDisLike, bool IsDisLike)> DisLikeAskAsync(InpDisLikeAsk Input);
        Task<(int CountLike, bool IsLike)> LikeAskAsync(InpLikeAsk Input);
    }
}