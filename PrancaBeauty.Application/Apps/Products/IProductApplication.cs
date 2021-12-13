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
        Task<OutGetForEdit> GetForEditAsync(InpGetForEdit Input);
        Task<(OutPagingData, List<OutGetProductsForManage>)> GetProductsForManageAsync(InpGetProductsForManage Input);
        Task<OperationResult> MoveToRecycleBinAsync(InpMoveToRecycleBin Input);
        Task<OperationResult> RecoveryFromRecycleBinAsync(InpRecoveryFromRecycleBin Input);
        Task<OperationResult> RemoveProductForAlwaysAsync(InpRemoveProductForAlways Input);
    }
}