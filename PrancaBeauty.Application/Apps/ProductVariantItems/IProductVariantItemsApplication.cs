using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariantItems
{
    public interface IProductVariantItemsApplication
    {
        Task<OperationResult> AddVariantsToProductAsync(InpAddVariantsToProduct Input);
        Task<OperationResult> RemoveAllVariantsFromProductAsync(InpRemoveVariantsFromProduct Input);
    }
}