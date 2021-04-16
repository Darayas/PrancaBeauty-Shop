using PrancaBeauty.Application.Contracts.Roles;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Roles
{
    public interface IRoleApplication
    {
        Task<List<string>> GetRolesByUserAsync(tblUsers user);
        Task<List<OutListOfRoles>> ListOfRolesAsync(string ParentId = null);
    }
}