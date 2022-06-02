using PrancaBeauty.Application.Contracts.ApplicationDTO.Cart;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Carts
{
    public interface ICartApplication
    {
        Task<OperationResult> AddToCartAsync(InpAddToCart Input);
        Task<OutGetItemsInCart> GetItemsInCartAsync(InpGetItemsInCart Input);
        Task<OperationResult> RemoveCartItemAsync(InpRemoveCartItem Input);
    }
}