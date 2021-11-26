using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.Categories;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Categories
{
    public interface ICategoryApplication
    {
        Task<OperationResult> AddCategoryAsync(InpAddCategory Input);
        Task<OutGetForEdit> GetForEditAsync(InpGetForEdit Input);
        Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(InpGetListForAdminPage Input);
        Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
        Task<IEnumerable<OutGetParentsByChildId>> GetParentsByChildIdAsync(InpGetParentsByChildId Input);
        Task<OperationResult> RemoveCategoryAsync(InpRemoveCategory Input);
        Task<OperationResult> SaveEditAsync(InpSaveEdit Input);
    }
}