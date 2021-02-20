using Microsoft.EntityFrameworkCore;
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
        private readonly IAccesslevelRepository _AccessLevelRepository;
        public AccesslevelApplication(IAccesslevelRepository accessLevelRepository)
        {
            _AccessLevelRepository = accessLevelRepository;
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
    }
}
