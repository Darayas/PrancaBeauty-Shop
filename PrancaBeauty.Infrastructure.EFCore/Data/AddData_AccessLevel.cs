using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_AccessLevel
    {
        BaseRepository<tblAccessLevels> _repAccessLevel;
        BaseRepository<tblRoles> _repRoles;
        public AddData_AccessLevel()
        {
            _repAccessLevel = new BaseRepository<tblAccessLevels>(new MainContext());
            _repRoles = new BaseRepository<tblRoles>(new MainContext());
        }

        public void Run()
        {
            if (!_repAccessLevel.Get.Any(a => a.Name == "GeneralManager"))
            {
                var qAccRole = new tblAccessLevels()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "GeneralManager",
                    tblAccessLevel_Roles = _repRoles.Get.Select(x=> new tblAccessLevel_Roles {
                        Id = new Guid().SequentialGuid(),
                        RoleId = x.Id
                    }).ToList()
                };

                _repAccessLevel.AddAsync(qAccRole, default, false).Wait();
            }

            if (!_repAccessLevel.Get.Any(a => a.Name == "Users"))
            {
                _repAccessLevel.AddAsync(new tblAccessLevels()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "Users"
                }, default, false).Wait();
            }


            _repAccessLevel.SaveChangeAsync().Wait();
        }
    }
}
