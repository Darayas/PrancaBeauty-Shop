using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Apps.AccesslevelsRoles;
using PrancaBeauty.Application.Apps.Roles;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.AccessLevels;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Exceptions;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Contracts;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Accesslevels
{
    public class AccesslevelApplication : IAccesslevelApplication
    {
        private readonly ILogger _Logger;
        private readonly IAccesslevelRepository _AccessLevelRepository;
        private readonly IAccesslevelRolesApplication _AccessLevelRolesApplication;
        private readonly IRoleApplication _RoleApplication;
        public AccesslevelApplication(IAccesslevelRepository accessLevelRepository, ILogger logger, IAccesslevelRolesApplication accessLevelRolesApplication, IRoleApplication roleApplication)
        {
            _AccessLevelRepository = accessLevelRepository;
            _Logger = logger;
            _AccessLevelRolesApplication = accessLevelRolesApplication;
            _RoleApplication = roleApplication;
        }

        public async Task<string> GetIdByNameAsync(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException("Name cant be null.");

            var qData = await _AccessLevelRepository.GetNoTraking.Where(a => a.Name == Name).Select(a => a.Id.ToString()).SingleOrDefaultAsync();

            if (qData == null)
                return Guid.Empty.ToString();

            return qData;
        }

        public async Task<(OutPagingData, List<OutGetListForAdminPage>)> GetListForAdminPageAsync(string Title, int PageNum, int Take)
        {
            try
            {
                if (PageNum < 1)
                    throw new ArgumentInvalidException("PageNum < 1");

                if (Take < 1)
                    throw new ArgumentInvalidException("Take < 1");

                Title = string.IsNullOrWhiteSpace(Title) ? null : Title;

                // آماده سازی اولیه ی کویری
                var qData = _AccessLevelRepository.Get.Select(a => new OutGetListForAdminPage
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                    CountUser = a.tblUsers.Count()
                })
                .Where(a => Title != null ? a.Name.Contains(Title) : true)
                .OrderBy(a => a.Name);

                // صفحه بندی داده ها
                var qPagingData = PagingData.Calc(await qData.LongCountAsync(), PageNum, Take);

                return (qPagingData, await qData.Skip((int)qPagingData.Skip).Take(qPagingData.Take).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
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
                if (string.IsNullOrWhiteSpace(Input.Name))
                    throw new ArgumentInvalidException("Name cant be null.", new ArgumentNullException());

                if (Input.Roles == null)
                    throw new ArgumentInvalidException("Roles cant be null.", new ArgumentNullException());

                if (await _AccessLevelRepository.Get.AnyAsync(a => a.Name == Input.Name))
                    throw new ArgumentInvalidException("Name is duplicate.", new ArgumentNullException());

                await _AccessLevelRepository.AddAsync(new tblAccessLevels
                {
                    Id = new Guid().SequentialGuid(),
                    Name = Input.Name,
                    tblAccessLevel_Roles = Input.Roles.Select(roleId => new tblAccessLevel_Roles()
                    {
                        Id = new Guid().SequentialGuid(),
                        RoleId = Guid.Parse(roleId)
                    }).ToList()
                }, default, true);

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

        public async Task<OperationResult> RemoveAsync(InpRemove Input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Input.Id))
                    throw new ArgumentInvalidException("Id cant be null.");

                // واکشی سطح دسترسی
                var qData = await _AccessLevelRepository.Get.Where(a => a.Id == Guid.Parse(Input.Id)).SingleOrDefaultAsync();
                if (qData == null)
                    return new OperationResult().Failed("AccessLevelNotFound");

                // برسی عضو بودن کاربر در سطح دسترسی جاری
                if (await CheckHasUserAsync(Input.Id))
                    return new OperationResult().Failed("AccessLevelHasUser");

                // حذف کامل سطح دسترسی
                await _AccessLevelRepository.DeleteAsync(qData, default, true);

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

        private async Task<bool> CheckHasUserAsync(string AccessLevelId)
        {
            return await _AccessLevelRepository.Get
                                           .Where(a => a.Id == Guid.Parse(AccessLevelId))
                                           .Select(a => a.tblUsers.Any())
                                           .SingleAsync();
        }

        public async Task<OutGetForEdit> GetForEditAsync(string AccessLevelId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(AccessLevelId))
                    throw new ArgumentInvalidException("AccessLevelId cant be null.");

                return await _AccessLevelRepository.Get
                                                   .Where(a => a.Id == Guid.Parse(AccessLevelId))
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
                if (Input == null)
                    throw new ArgumentInvalidException("Input cant be null.");

                if (string.IsNullOrWhiteSpace(Input.Id))
                    throw new ArgumentInvalidException("Id cant be null.");

                if (string.IsNullOrWhiteSpace(Input.Name))
                    throw new ArgumentInvalidException("Name cant be null.");

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
                var ResultRemoveAccRoles = await _AccessLevelRolesApplication.RemoveByAccessLevelIdAsync(Input.Id);

                // ثبت رول های جدید
                await _AccessLevelRolesApplication.AddRolesToAccessLevelAsync(Input.Id, Input.Roles);

                // ثبت ویرایش
                await _AccessLevelRepository.UpdateAsync(qData, default, true);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name">بخشی از نام برای جستوجو</param>
        /// <returns></returns>
        public async Task<List<OutGetForChangeUserAccesssLevel>> GetForChangeUserAccesssLevelAsync(string Name)
        {
            var qData = await _AccessLevelRepository.Get
                                                    .Select(a => new OutGetForChangeUserAccesssLevel
                                                    {
                                                        Id = a.Id.ToString(),
                                                        Name = a.Name
                                                    })
                                                    .Where(a => Name != null ? a.Name.Contains(Name) : true)
                                                    .ToListAsync();

            return qData;

        }
    }
}
