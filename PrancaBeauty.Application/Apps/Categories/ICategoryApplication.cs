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
        Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(string LangId, string Title, string ParentTitle, int PageNum, int Take);
        Task<List<OutGetListForCombo>> GetListForComboAsync(string LangId, string ParentId);
        Task<OperationResult> RemoveAsync(string Id);
    }
}