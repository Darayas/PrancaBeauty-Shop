using Framework.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Roles;
using PrancaBeauty.Domin.Users.RoleAgg.Contracts;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Roles
{
    public class RoleApplication : IRoleApplication
    {
        private readonly ILogger _Logger;
        private readonly IRoleRepository _RoleRepository;
        private readonly RoleManager<tblRoles> _RoleManager;
        private readonly UserManager<tblUsers> _UserManager;
        public RoleApplication(IRoleRepository roleRepository, ILogger logger, RoleManager<tblRoles> roleManager, UserManager<tblUsers> userManager)
        {
            _RoleRepository = roleRepository;
            _Logger = logger;
            _RoleManager = roleManager;
            _UserManager = userManager;
        }

        public async Task<List<string>> GetRolesByUserAsync(tblUsers user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException("User cant be null.");

                return (await _UserManager.GetRolesAsync(user)).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<List<OutListOfRoles>> ListOfRolesAsync(string ParentId = null)
        {
            var qData = await _RoleManager.Roles
                                         .Where(a => a.ParentId == (ParentId == null ? null : Guid.Parse(ParentId)))
                                         .Select(a => new OutListOfRoles
                                         {
                                             Id = a.Id.ToString(),
                                             Name = a.Name,
                                             PageName = a.PageName,
                                             Description = a.Description,
                                             Sort = a.Sort,
                                             HasChild = a.tblRoles_Childs.Any(),
                                             ParentId = a.ParentId.HasValue ? a.ParentId.Value.ToString() : null
                                         })
                                         .ToListAsync();

            return qData;

        }
    }
}
