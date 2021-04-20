using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.AccessLevels;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Accesslevels
{
    public interface IAccesslevelApplication
    {
        Task<OperationResult> AddNewAsync(InpAddNewAccessLevel Input);
        Task<string> GetIdByNameAsync(string Name);
        Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(string Title, int PageNum, int Take);
        Task<OperationResult> RemoveAsync(InpRemove Input);
    }
}