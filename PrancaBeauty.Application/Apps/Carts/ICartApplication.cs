using PrancaBeauty.Application.Contracts.ApplicationDTO.Cart;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Carts
{
    public interface ICartApplication
    {
        Task<OperationResult> AddToCartAsync(InpAddToCart Input);
        Task<OperationResult> ChangeQtyCartAsync(InpChangeQtyCart Input);
        Task<OperationResult> ClearCartAsync(InpClearCart Input);
        Task<List<OutGetItemsForBill>> GetItemsForBillAsync(InpGetItemsForBill Input);
        Task<OutGetItemsInCart> GetItemsInCartAsync(InpGetItemsInCart Input);
        Task<OperationResult> RemoveCartItemAsync(InpRemoveCartItem Input);
    }
}