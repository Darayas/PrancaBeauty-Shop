using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Roles;
using PrancaBeauty.Application.Contracts.AccesslevelRoles;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Contracts.Roles;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Contracts;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.AccesslevelsRoles
{
    public class AccesslevelRolesApplication : IAccesslevelRolesApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IAccesslevelRolesRepository _AccesslevelRolesRepository;
        private readonly IRoleApplication _RoleApplication;
        public AccesslevelRolesApplication(IAccesslevelRolesRepository accesslevelRolesRepository, ILogger logger, IRoleApplication roleApplication, ILocalizer localizer)
        {
            _AccesslevelRolesRepository = accesslevelRolesRepository;
            _Logger = logger;
            _RoleApplication = roleApplication;
            _Localizer = localizer;
        }

        public async Task<OperationResult> RemoveByAccessLevelIdAsync(InpRemoveByAccessLevelId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_Localizer);
                #endregion

                // واکشی اطلاعات
                var qData = await _AccesslevelRolesRepository.Get.Where(a => a.AccessLevelId == Guid.Parse(Input.AccessLevelId)).ToListAsync();
                foreach (var item in qData)
                {
                    // حذف رکورد ها
                    await _AccesslevelRolesRepository.DeleteAsync(item, default, false);
                }

                // ذخیره تغییرات
                await _AccesslevelRolesRepository.SaveChangeAsync();

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> AddRolesToAccessLevelAsync(InpAddRolesToAccessLevel Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_Localizer);

                if (Input.RolesName == null)
                    throw new ArgumentInvalidException("RolesName cant be null.");
                #endregion


                foreach (var item in Input.RolesName)
                {
                    await _AccesslevelRolesRepository.AddAsync(new tblAccessLevel_Roles
                    {
                        Id = new Guid().SequentialGuid(),
                        AccessLevelId = Guid.Parse(Input.AccessLevelId),
                        RoleId = Guid.Parse(await _RoleApplication.GetIdByNameAsync(new InpGetIdByName { Name = item }))
                    }, default, false);
                }

                await _AccesslevelRolesRepository.SaveChangeAsync();

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
