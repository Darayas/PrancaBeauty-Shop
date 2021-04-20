using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.AccessLevels;
using PrancaBeauty.Application.Contracts.Results;
using PrancaBeauty.Application.Exceptions;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Contracts;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Accesslevels
{
    public class AccesslevelApplication : IAccesslevelApplication
    {
        private readonly ILogger _Logger;
        private readonly IAccesslevelRepository _AccessLevelRepository;
        public AccesslevelApplication(IAccesslevelRepository accessLevelRepository, ILogger logger)
        {
            _AccessLevelRepository = accessLevelRepository;
            _Logger = logger;
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
                    throw new Exception("PageNum < 1");

                if (Take < 1)
                    throw new Exception("Take < 1");

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
    }
}
