﻿using Framework.Common.Utilities.Paging;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.AccessLevels;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Contracts;
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
    }
}
