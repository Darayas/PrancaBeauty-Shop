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
        Task<OperationResult> ChangeStatusProductSellerAsync(InpChangeStatusProductSeller Input);
        Task<(OutPagingData, List<vmGetAllSellerForManageByProductId>)> GetAllSellerForManageByProductIdAsync(InpGetAllSellerForManageByProductId Input);
        Task<string> GetProductSellerIdAsync(InpGetProductSellerId Input);
        Task<string> GetSellerIdByProductSellerIdAsync(InpGetSellerIdByProductSellerId Input);
        Task<string> GetUserIdByProductSellerIdAsync(InpGetUserIdByProductSellerId Input);
        Task<OperationResult> RemoveProductSellerAsync(InpRemoveProductSeller Input);
    }
}