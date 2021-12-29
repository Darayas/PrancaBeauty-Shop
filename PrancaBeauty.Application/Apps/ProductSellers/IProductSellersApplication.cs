using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductSellers
{
    public interface IProductSellersApplication
    {
        Task<OperationResult> AddSellerToProdcutAsync(InpAddSellerToProdcut Input);
        Task<string> GetSellerIdAsync(InpGetSellerId Input);
        Task<OperationResult> RemoveAllPriceFromProductAsync(InpRemoveAllPriceFromProduct Input);
    }
}