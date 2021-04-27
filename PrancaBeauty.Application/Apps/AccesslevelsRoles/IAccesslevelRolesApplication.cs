using PrancaBeauty.Application.Contracts.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.AccesslevelsRoles
{
    public interface IAccesslevelRolesApplication
    {
        Task<OperationResult> RemoveByAccessLevelIdAsync(string AccessLevelId);
    }
}