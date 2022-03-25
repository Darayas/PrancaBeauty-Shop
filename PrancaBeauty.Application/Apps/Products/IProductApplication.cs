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
        Task<(bool IsConfirm, OutGetProductForDetails Product)> GetProductForDetailsAsync(InpGetProductForDetails Input);
        Task<OutGetProductPriceByVariantId> GetProductPriceByVariantIdAsync(InpGetProductPriceByVariantId Input);
        Task<(OutPagingData, List<OutGetProductsForManage>)> GetProductsForManageAsync(InpGetProductsForManage Input);
        Task<OutGetSummaryById> GetSummaryByIdAsync(InpGetSummaryById Input);
        Task<OperationResult> MoveToRecycleBinAsync(InpMoveToRecycleBin Input);
        Task<OperationResult> RecoveryFromRecycleBinAsync(InpRecoveryFromRecycleBin Input);
        Task<OperationResult> RemoveProductForAlwaysAsync(InpRemoveProductForAlways Input);
        Task<OperationResult> SaveEditProductAsync(InpSaveEditProduct Input);
    }
}