using PrancaBeauty.Application.Contracts.Roles;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Roles
{
    public interface IRoleApplication
    {
        Task<string> GetIdByNameAsync(InpGetIdByName Input);
        Task<List<string>> GetRolesByUserAsync(InpGetRolesByUser Input);
        Task<List<OutListOfRoles>> ListOfRolesAsync(InpListOfRoles Input);
        Task<string[]> ListOfRolesByAccessLevelIdAsync(InpListOfRolesByAccessLevelId Input);
    }
}