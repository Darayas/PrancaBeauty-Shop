using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductPropertiesValues;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariantItems
{
    public interface IProductVariantItemsApplication
    {
        Task<OperationResult> AddVariantsToProductAsync(InpAddVariantsToProduct Input);
        Task<OperationResult> ChangeStatusVariantItemAsync(InpChangeStatusVariantItem Input);
        Task<bool?> CheckHasPurchaseForVariantAsync(InpCheckHasPurchaseForVariant Input);
        Task<OperationResult> EditProductVariantsAsync(InpEditProductVariants Input);
        Task<List<OutGetAllProductSellerByVariantValue>> GetAllProductSellerByVariantValueAsync(InpGetAllProductSellerByVariantValue Input);
        Task<OutGetAllProductVariantsForProductDetails> GetAllProductVariantsForProductDetailsAsync(InpGetAllProductVariantsForProductDetails Input);
        Task<List<OutGetAllVariantsByProductId>> GetAllVariantsByProductIdAsync(InpGetAllVariantsByProductId Input);
        Task<OutGetProductPriceByVariantItemId> GetProductPriceByVariantItemIdAsync(InpGetProductPriceByVariantItemId Input);
        Task<string> GetProductVariantIdAsync(InpGetProductVariantId Input);
        Task<OperationResult> RemoveAllVariantsFromProductAsync(InpRemoveVariantsFromProduct Input);
    }
}