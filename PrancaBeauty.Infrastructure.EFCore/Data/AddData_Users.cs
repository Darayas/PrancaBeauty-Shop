using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Identity;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Users
    {
        BaseRepository<tblUsers> _repUser;
        BaseRepository<tblAccessLevels> _repAccessLevel;
        BaseRepository<tblRoles> _repRoles;

        MainContext _Context;
        public AddData_Users()
        {
            _repUser = new BaseRepository<tblUsers>(new MainContext());
            _repAccessLevel = new BaseRepository<tblAccessLevels>(new MainContext());
            _repRoles = new BaseRepository<tblRoles>(new MainContext());

            _Context = new MainContext();
        }

        public void Run()
        {
            if (!_repUser.Get.Any(a => a.UserName == "reza9025@gmail.com"))
            {
                Guid UserId = new Guid().SequentialGuid();
                _repUser.AddAsync(new tblUsers()
                {
                    Id = UserId,
                    AccessLevelId = _repAccessLevel.Get.Where(a => a.Name == "GeneralManager").Select(a => a.Id).Single(),
                    FirstName = "محمدرضا",
                    LastName = "احمدی",
                    IsActive = true,
                    Date = DateTime.Now,
                    UserName = "reza9025@gmail.com",
                    NormalizedUserName = "reza9025@gmail.com".ToUpper(),
                    Email = "reza9025@gmail.com",
                    NormalizedEmail = "reza9025@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEO3Ro+1N3qaDwJUK02Qih+FlDMKZxhO0Z2JPMgd3rgrQSPeFLQh3txpgkEvlFMRUXA==", // 123456
                    SecurityStamp = "QHZXXDN4PZUNNXGC6LQRVNOZ5EGGIKWH",
                    ConcurrencyStamp = "37116a3b-0da5-460e-b266-d5243f62e5c8",
                    PhoneNumber = "09010112829",
                    PhoneNumberConfirmed = true,
                    IsSeller = true
                }, default, true).Wait();


                foreach (var item in _repRoles.Get.ToList())
                {
                    _Context.Add(new IdentityUserRole<Guid>
                    {
                        UserId = UserId,
                        RoleId = item.Id
                    });
                }

                _Context.SaveChanges();
            }

            if (!_repUser.Get.Any(a => a.UserName == "test9025@gmail.com"))
            {
                Guid UserId = new Guid().SequentialGuid();
                _repUser.AddAsync(new tblUsers()
                {
                    Id = UserId,
                    AccessLevelId = _repAccessLevel.Get.Where(a => a.Name == "GeneralManager").Select(a => a.Id).Single(),
                    FirstName = "حسن",
                    LastName = "حسینی",
                    IsActive = true,
                    Date = DateTime.Now,
                    UserName = "test9025@gmail.com",
                    NormalizedUserName = "test9025@gmail.com".ToUpper(),
                    Email = "test9025@gmail.com",
                    NormalizedEmail = "test9025@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEO3Ro+1N3qaDwJUK02Qih+FlDMKZxhO0Z2JPMgd3rgrQSPeFLQh3txpgkEvlFMRUXA==", // 123456
                    SecurityStamp = "QHZXXDN4PZUNNXGC6LQRVNOZ5EGGIKWH",
                    ConcurrencyStamp = "37116a3b-0da5-460e-b266-d5243f62e5c8",
                    PhoneNumber = "09147922542",
                    PhoneNumberConfirmed = true,
                    IsSeller = true
                }, default, true).Wait();


                foreach (var item in _repRoles.Get.ToList())
                {
                    _Context.Add(new IdentityUserRole<Guid>
                    {
                        UserId = UserId,
                        RoleId = item.Id
                    });
                }

                _Context.SaveChanges();
            }
        }
    }
}
