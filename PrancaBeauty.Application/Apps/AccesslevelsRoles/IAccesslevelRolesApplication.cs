using PrancaBeauty.Application.Contracts.Results;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.AccesslevelsRoles
{
    public interface IAccesslevelRolesApplication
    {
        Task<OperationResult> AddRolesToAccessLevelAsync(string AccessLevelId, string[] RolesName);
        Task<OperationResult> RemoveByAccessLevelIdAsync(string AccessLevelId);
    }
}