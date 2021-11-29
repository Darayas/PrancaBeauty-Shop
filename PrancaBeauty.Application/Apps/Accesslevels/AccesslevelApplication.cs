using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.AccesslevelsRoles;
using PrancaBeauty.Application.Apps.Roles;
using PrancaBeauty.Application.Contracts.AccesslevelRoles;
using PrancaBeauty.Application.Contracts.AccessLevels;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Contracts;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Accesslevels
{
    public class AccesslevelApplication : IAccesslevelApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IAccesslevelRepository _AccessLevelRepository;
        private readonly IAccesslevelRolesApplication _AccessLevelRolesApplication;
        private readonly IRoleApplication _RoleApplication;
        public AccesslevelApplication(IAccesslevelRepository accessLevelRepository, ILogger logger, IAccesslevelRolesApplication accessLevelRolesApplication, IRoleApplication roleApplication, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _AccessLevelRepository = accessLevelRepository;
            _Logger = logger;
            _AccessLevelRolesApplication = accessLevelRolesApplication;
            _RoleApplication = roleApplication;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<string> GetIdByNameAsync(InpGetIdByName Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            if (string.IsNullOrWhiteSpace(Input.Name))
                throw new ArgumentNullException("Name cant be null.");

            var qData = await _AccessLevelRepository.GetNoTraking.Where(a => a.Name == Input.Name).Select(a => a.Id.ToString()).SingleOrDefaultAsync();

            if (qData == null)
                return Guid.Empty.ToString();

            return qData;
        }

        public async Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(InpGetListForAdminPage Input)
        {
            try
            {
                #region Validatitions
                Input.CheckModelState(_ServiceProvider);
                #endregion

                Input.Title = string.IsNullOrWhiteSpace(Input.Title) ? null : Input.Title;

                // آماده سازی اولیه ی کویری
                var qData = _AccessLevelRepository.Get.Select(a => new OutGetListForAdminPage
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                    CountUser = a.tblUsers.Count()
                })
                .Where(a => Input.Title != null ? a.Name.Contains(Input.Title) : true)
                .OrderBy(a => a.Name);

                // صفحه بندی داده ها
                var qPagingData = PagingData.Calc(await qData.LongCountAsync(), Input.PageNum, Input.Take);

                return (qPagingData, await qData.Skip((int)qPagingData.Skip).Take(qPagingData.Take).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return (null, null);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (null, null);
            }
        }

        public async Task<OperationResult> AddNewAsync(InpAddNewAccessLevel Input)
        {
            try
            {

                #region Validations
                Input.CheckModelState(_ServiceProvider);

                if (Input.Roles == null)
                    throw new ArgumentInvalidException("Roles cant be null.");

                if (await _AccessLevelRepository.Get.AnyAsync(a => a.Name == Input.Name))
                    throw new ArgumentInvalidException("Name is duplicate.");

                #endregion

                await _AccessLevelRepository.AddAsync(new tblAccessLevels
                {
                    Id = new Guid().SequentialGuid(),
                    Name = Input.Name,
                    tblAccessLevel_Roles = Input.Roles.Select(role => new tblAccessLevel_Roles()
                    {
                        Id = new Guid().SequentialGuid(),
                        RoleId = Guid.Parse(_RoleApplication.GetIdByNameAsync(new Contracts.Roles.InpGetIdByName { Name = role }).Result)
                    }).ToList()
                }, default, true);

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

        public async Task<OperationResult> RemoveAsync(InpRemove Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                // واکشی سطح دسترسی
                var qData = await _AccessLevelRepository.Get.Where(a => a.Id == Guid.Parse(Input.Id)).SingleOrDefaultAsync();
                if (qData == null)
                    return new OperationResult().Failed("AccessLevelNotFound");

                // برسی عضو بودن کاربر در سطح دسترسی جاری
                if (await CheckHasUserAsync(new InpCheckHasUser() { AccessLevelId = Input.Id }))
                    return new OperationResult().Failed("AccessLevelHasUser");

                // حذف کامل سطح دسترسی
                await _AccessLevelRepository.DeleteAsync(qData, default, true);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        private async Task<bool> CheckHasUserAsync(InpCheckHasUser Input)
        {
            #region Validations
            Input.CheckModelState(_ServiceProvider);
            #endregion

            return await _AccessLevelRepository.Get
                                           .Where(a => a.Id == Guid.Parse(Input.AccessLevelId))
                                           .Select(a => a.tblUsers.Any())
                                           .SingleAsync();
        }

        public async Task<OutGetForEdit> GetForEditAsync(InpGetForEdit Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                return await _AccessLevelRepository.Get
                                                   .Where(a => a.Id == Guid.Parse(Input.AccessLevelId))
                                                   .Select(a => new OutGetForEdit
                                                   {
                                                       Id = a.Id.ToString(),
                                                       Name = a.Name
                                                   })
                                                   .SingleOrDefaultAsync();
            }
            catch (ArgumentInvalidException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<OperationResult> UpdateAsync(InpUpdateAccessLevel Input)
        {
            try
            {
                #region برسی ورودی ها
                Input.CheckModelState(_ServiceProvider);

                if (Input.Roles == null)
                    throw new ArgumentInvalidException("Roles cant be null.");

                if (Input.Roles.Count() == 0)
                    throw new ArgumentInvalidException("Roles cant be null.");
                #endregion

                // واکشی اطلاعات قدیمی
                var qData = await _AccessLevelRepository.Get.Where(a => a.Id == Guid.Parse(Input.Id)).SingleOrDefaultAsync();
                if (qData == null)
                    return new OperationResult().Failed("Error404");

                // جایگزاری داده های جدید
                qData.Name = Input.Name;

                // لغو عضویت تمامی رول ها
                var ResultRemoveAccRoles = await _AccessLevelRolesApplication.RemoveByAccessLevelIdAsync(new InpRemoveByAccessLevelId() { AccessLevelId = Input.Id });

                // ثبت رول های جدید
                await _AccessLevelRolesApplication.AddRolesToAccessLevelAsync(new InpAddRolesToAccessLevel { AccessLevelId = Input.Id, RolesName = Input.Roles });

                // ثبت ویرایش
                await _AccessLevelRepository.UpdateAsync(qData, default, true);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name">بخشی از نام برای جستوجو</param>
        /// <returns></returns>
        public async Task<List<OutGetForChangeUserAccesssLevel>> GetForChangeUserAccesssLevelAsync(InpGetForChangeUserAccesssLevel Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _AccessLevelRepository.Get
                                                    .Select(a => new OutGetForChangeUserAccesssLevel
                                                    {
                                                        Id = a.Id.ToString(),
                                                        Name = a.Name
                                                    })
                                                    .Where(a => Input.Name != null ? a.Name.Contains(Input.Name) : true)
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

        public async Task<List<string>> GetRolesNameByAccIdAsync(InpGetRolesNameByAccId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                return await _AccessLevelRepository.Get
                                               .Where(a => a.Id == Guid.Parse(Input.AccessLevelId))
                                               .Select(a => a.tblAccessLevel_Roles.Select(b => b.tblRoles.Name).ToList())
                                               .SingleAsync();
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
