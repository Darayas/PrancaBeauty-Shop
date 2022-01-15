using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ProductSellers;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductSellers
{
    public interface IProductSellersApplication
    {
        Task<OperationResult> AddSellerToProdcutAsync(InpAddSellerToProdcut Input);
        Task<OperationResult> AddSellerWithVariantsToProdcutAsync(InpAddSellerWithVariantsToProdcut Input);
        Task<(OutPagingData, List<vmGetAllSellerForManageByProductId>)> GetAllSellerForManageByProductIdAsync(InpGetAllSellerForManageByProductId Input);
        Task<string> GetSellerIdAsync(InpGetSellerId Input);
        Task<string> GetUserIdByProductSellerIdAsync(InpGetUserIdByProductSellerId Input);
        Task<OperationResult> RemoveProductSellerAsync(InpRemoveProductSeller Input);
    }
}