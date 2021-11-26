using Framework.Common.ExMethods;
using Framework.Exceptions;
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

        public async Task<List<string>> GetRolesByUserAsync(InpGetRolesByUser Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState();
                #endregion

                var qUser = await _UserManager.FindByIdAsync(Input.UserId);
                if (qUser == null)
                    return null;

                return (await _UserManager.GetRolesAsync(qUser)).ToList();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<List<OutListOfRoles>> ListOfRolesAsync(InpListOfRoles Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState();
                #endregion

                var qData = await _RoleManager.Roles
                                             .Where(a => a.ParentId == (Input.ParentId == null ? null : Guid.Parse(Input.ParentId)))
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
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }

        }

        public async Task<string[]> ListOfRolesByAccessLevelIdAsync(InpListOfRolesByAccessLevelId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState();
                #endregion

                var qData = await _RoleManager.Roles
                           .Where(a => a.tblAccessLevel_Roles.Where(b => b.AccessLevelId == Guid.Parse(Input.AccessLevelId)).Any())
                           .Select(a => a.Name.ToString())
                           .ToArrayAsync();

                return qData;
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<string> GetIdByNameAsync(InpGetIdByName Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState();
                #endregion

                return await _RoleRepository.Get.Where(a => a.Name == Input.Name).Select(a => a.Id.ToString()).SingleOrDefaultAsync();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
