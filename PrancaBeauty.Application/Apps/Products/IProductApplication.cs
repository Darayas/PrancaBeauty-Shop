using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Products
{
    public interface IProductApplication
    {
        Task<OperationResult> AddProdcutAsync(InpAddProdcut Input);
        Task<(OutPagingData, List<OutGetProductsForManage>)> GetProductsForManageAsync(int Page, int Take, string LangId, string SellerUserId, string AuthorUserId, string Title, string Name, bool? IsDelete, bool? IsDraft, bool? IsConfirmed, bool? IsSchedule);
    }
}