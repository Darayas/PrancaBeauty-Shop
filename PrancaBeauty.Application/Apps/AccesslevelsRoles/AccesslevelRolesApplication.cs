﻿using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.Roles;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Exceptions;
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
        private readonly IAccesslevelRolesRepository _AccesslevelRolesRepository;
        private readonly IRoleApplication _RoleApplication;
        public AccesslevelRolesApplication(IAccesslevelRolesRepository accesslevelRolesRepository, ILogger logger, IRoleApplication roleApplication)
        {
            _AccesslevelRolesRepository = accesslevelRolesRepository;
            _Logger = logger;
            _RoleApplication = roleApplication;
        }

        public async Task<OperationResult> RemoveByAccessLevelIdAsync(string AccessLevelId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(AccessLevelId))
                    throw new ArgumentInvalidException("AccessLevelId cant be null.");

                // واکشی اطلاعات
                var qData = await _AccesslevelRolesRepository.Get.Where(a => a.AccessLevelId == Guid.Parse(AccessLevelId)).ToListAsync();
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
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> AddRolesToAccessLevelAsync(string AccessLevelId, string[] RolesName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(AccessLevelId))
                    throw new ArgumentInvalidException("AccessLevelId cant be null.");

                if (RolesName == null)
                    throw new ArgumentInvalidException("RolesName cant be null.");

                foreach (var item in RolesName)
                {
                    await _AccesslevelRolesRepository.AddAsync(new tblAccessLevel_Roles
                    {
                        Id = new Guid().SequentialGuid(),
                        AccessLevelId = Guid.Parse(AccessLevelId),
                        RoleId = Guid.Parse(await _RoleApplication.GetIdByNameAsync(item))
                    }, default, false);
                }

                await _AccesslevelRolesRepository.SaveChangeAsync();

                return new OperationResult().Succeeded();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
