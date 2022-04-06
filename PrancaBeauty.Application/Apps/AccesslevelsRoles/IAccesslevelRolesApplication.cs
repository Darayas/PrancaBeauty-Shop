using PrancaBeauty.Application.Contracts.ApplicationDTO.AccesslevelRoles;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.AccesslevelsRoles
{
    public interface IAccesslevelRolesApplication
    {
        Task<OperationResult> AddRolesToAccessLevelAsync(InpAddRolesToAccessLevel Input);
        Task<OperationResult> RemoveByAccessLevelIdAsync(InpRemoveByAccessLevelId Input);
    }
}