using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Categories;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Categories
{
    public interface ICategoryApplication
    {
        Task<OperationResult> AddCategoryAsync(InpAddCategory Input);
        Task<List<OutGetCategoriesForSeachAutoComplete>> GetCategoriesForSeachAutoCompleteAsync(InpGetCategoriesForSeachAutoComplete Input);
        Task<OutGetCategoryDetailsByName> GetCategoryDetailsByNameAsync(InpGetCategoryDetailsByName Input);
        Task<OutGetCategoryForEdit> GetCategoryForEditAsync(InpGetForEdit Input);
        Task<string> GetIdByCategoryNameAsync(InpGetIdByCategoryName Input);
        Task<(OutPagingData, List<OutGetCategoryListForAdminPage>)> GetListForAdminPageAsync(InpGetListForAdminPage Input);
        Task<List<OutGetCategoryListForCombo>> GetListForComboAsync(InpGetListForCombo Input);
        Task<IEnumerable<OutGetParentsByChildId>> GetParentsByChildIdAsync(InpGetParentsByChildId Input);
        Task<string> GetTopicIdByCateNameAsync(InpGetTopicIdByCateName Input);
        Task<OperationResult> RemoveCategoryAsync(InpRemoveCategory Input);
        Task<OperationResult> SaveEditAsync(InpSaveEdit Input);
    }
}