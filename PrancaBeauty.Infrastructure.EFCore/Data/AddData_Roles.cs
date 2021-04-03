using Framework.Infrastructure;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Common.ExMethods;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Roles
    {
        BaseRepository<tblRoles> _repRoles;
        public AddData_Roles()
        {
            _repRoles = new BaseRepository<tblRoles>(new MainContext());
        }
        public void Run()
        {
            if (!_repRoles.Get.Any(a => a.Name == "FullControl"))
            {
                _repRoles.AddAsync(new tblRoles()
                {
                    Id = new Guid().SequentialGuid(),
                    ParentId = null,
                    PageName = "FullControl",
                    Sort = 0,
                    Name = "FullControl",
                    NormalizedName = "FullControl".ToUpper(),
                    Description = "دسترسی مدیر کل"
                }, default, false).Wait();
            }

            _repRoles.SaveChangeAsync().Wait();
        }
    }
}
