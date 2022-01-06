using PrancaBeauty.Application.Contracts.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariantItems
{
    public interface IProductVariantItemsApplication
    {
        Task<OperationResult> AddVariantsToProductAsync(InpAddVariantsToProduct Input);
        Task<bool?> CheckHasPurchaseForVariantAsync(InpCheckHasPurchaseForVariant Input);
        Task<OperationResult> EditProductVariantsAsync(InpEditProductVariants Input);
        Task<List<OutGetAllVariantsByProductId>> GetAllVariantsByProductIdAsync(InpGetAllVariantsByProductId Input);
        Task<string> GetProductVariantAsync(InpGetProductVariant Input);
        Task<OperationResult> RemoveAllVariantsFromProductAsync(InpRemoveVariantsFromProduct Input);
    }
}