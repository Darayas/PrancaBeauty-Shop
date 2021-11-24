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
        Task<List<OutGetForChangeUserAccesssLevel>> GetForChangeUserAccesssLevelAsync(InpGetForChangeUserAccesssLevel Input);
        Task<OutGetForEdit> GetForEditAsync(InpGetForEdit Input);
        Task<string> GetIdByNameAsync(InpGetIdByName Input);
        Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(InpGetListForAdminPage Input);
        Task<List<string>> GetRolesNameByAccIdAsync(InpGetRolesNameByAccId Input);
        Task<OperationResult> RemoveAsync(InpRemove Input);
        Task<OperationResult> UpdateAsync(InpUpdateAccessLevel Input);
    }
}