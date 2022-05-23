using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Products
{
    public interface IProductApplication
    {
        Task<OperationResult> AddProdcutAsync(InpAddProdcut Input);
        Task<OutGetProductForEdit> GetProductForEditAsync(InpGetProductForEdit Input);
        Task<(bool IsConfirm, OutGetProductForDetails Product)> GetProductForDetailsAsync(InpGetProductForDetails Input);
        Task<List<OutGetProductListForCombo>> GetProductListForComboAsync(InpGetProductListForCombo Input);
        Task<OutGetProductPriceByVariantId> GetProductPriceByVariantValueAsync(InpGetProductPriceByVariantValue Input);
        Task<(OutPagingData, List<OutGetProductsForManage>)> GetProductsForManageAsync(InpGetProductsForManage Input);
        Task<OutGetSummaryById> GetSummaryByIdAsync(InpGetSummaryById Input);
        Task<OperationResult> MoveToRecycleBinAsync(InpMoveToRecycleBin Input);
        Task<OperationResult> RecoveryFromRecycleBinAsync(InpRecoveryFromRecycleBin Input);
        Task<OperationResult> RemoveProductForAlwaysAsync(InpRemoveProductForAlways Input);
        Task<OperationResult> SaveEditProductAsync(InpSaveEditProduct Input);
        Task<(OutPagingData PagingData, List<OutGetProductListForAdvanceSearch> LstProduct)> GetProductListForAdvanceSearchAsync(InpGetProductListForAdvanceSearch Input);
    }
}