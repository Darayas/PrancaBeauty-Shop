﻿using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.AccessLevels;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Accesslevels
{
    public interface IAccesslevelApplication
    {
        Task<OperationResult> AddNewAsync(InpAddNewAccessLevel Input);
        Task<List<OutGetForChangeUserAccesssLevel>> GetForChangeUserAccesssLevelAsync(string Name);
        Task<OutGetForEdit> GetForEditAsync(string AccessLevelId);
        Task<string> GetIdByNameAsync(string Name);
        Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(string Title, int PageNum, int Take);
        Task<List<string>> GetRolesNameByAccIdAsync(string AccessLevelId);
        Task<OperationResult> RemoveAsync(InpRemove Input);
        Task<OperationResult> UpdateAsync(InpUpdateAccessLevel Input);
    }
}