﻿using Framework.Common.ExMethods;
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
            if (!_repUser.Get.Any(a => a.UserName == "RezaAhmadi"))
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
                    UserName = "RezaAhmadi",
                    NormalizedUserName = "RezaAhmadi".ToUpper(),
                    Email = "reza9025@gmail.com",
                    NormalizedEmail = "reza9025@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAELVlHmEilr3l0mizs5GxKWdCQIL8ys6zuHdVro+hNsU7RxYC9HtJqdajundVGRFC5Q==",
                    SecurityStamp = "QHZXXDN4PZUNNXGC6LQRVNOZ5EGGIKWH",
                    ConcurrencyStamp = "37116a3b-0da5-460e-b266-d5243f62e5c8",
                    PhoneNumber = "9010112829",
                    PhoneNumberConfirmed = true,
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
