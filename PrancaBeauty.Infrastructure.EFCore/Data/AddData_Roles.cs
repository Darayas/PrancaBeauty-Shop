using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Users.RoleAgg.Entities;
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
            if (!_repRoles.Get.Any(a => a.Name == "AdminPage"))
            {
                _repRoles.AddAsync(new tblRoles()
                {
                    Id = new Guid().SequentialGuid(),
                    ParentId = null,
                    PageName = "AdminPage",
                    Sort = 0,
                    Name = "AdminPage",
                    NormalizedName = "AdminPage".ToUpper(),
                    Description = "دسترسی به پنل مدیریت"
                }, default, false).Wait();
            }

            #region ManageAccessLevelPage
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!_repRoles.Get.Any(a => a.Name == "CanManageAccessLevel"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "ManageAccessLevelPage",
                        Sort = 10,
                        Name = "CanManageAccessLevel",
                        NormalizedName = "CanManageAccessLevel".ToUpper(),
                        Description = "توانایی مدیریت سطوح دسترسی"
                    }, default, false).Wait();
                }
                else
                {
                    _Id = _repRoles.Get.Where(a => a.Name == "CanManageAccessLevel").Select(a => a.Id).Single();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListAccessLevel"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageAccessLevelPage",
                        Sort = 20,
                        Name = "CanViewListAccessLevel",
                        NormalizedName = "CanViewListAccessLevel".ToUpper(),
                        Description = "توانایی مشاهده لیست سطوح دسترسی"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanAddAccessLevel"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageAccessLevelPage",
                        Sort = 30,
                        Name = "CanAddAccessLevel",
                        NormalizedName = "CanAddAccessLevel".ToUpper(),
                        Description = "توانایی افزودن سطح دسترسی"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanEditAccessLevel"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageAccessLevelPage",
                        Sort = 40,
                        Name = "CanEditAccessLevel",
                        NormalizedName = "CanEditAccessLevel".ToUpper(),
                        Description = "توانایی ویرایش سطح دسترسی"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanRemoveAccessLevel"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageAccessLevelPage",
                        Sort = 50,
                        Name = "CanRemoveAccessLevel",
                        NormalizedName = "CanRemoveAccessLevel".ToUpper(),
                        Description = "توانایی حذف سطح دسترسی"
                    }, default, false).Wait();
                }
            }
            #endregion

            #region ManageUsersPage
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!_repRoles.Get.Any(a => a.Name == "CanManageUsers"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "ManageUsersPage",
                        Sort = 60,
                        Name = "CanManageUsers",
                        NormalizedName = "CanManageUsers".ToUpper(),
                        Description = "توانایی مدیریت کاربران"
                    }, default, false).Wait();
                }
                else
                {
                    _Id = _repRoles.Get.Where(a => a.Name == "CanManageUsers").Select(a => a.Id).Single();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListUsers"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 60,
                        Name = "CanViewListUsers",
                        NormalizedName = "CanViewListUsers".ToUpper(),
                        Description = "توانایی مشاهده لیست کاربران"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanAddUsers"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 70,
                        Name = "CanAddUsers",
                        NormalizedName = "CanAddUsers".ToUpper(),
                        Description = "توانایی افزودن کاربر جدید"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanEditUsers"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 80,
                        Name = "CanEditUsers",
                        NormalizedName = "CanEditUsers".ToUpper(),
                        Description = "توانایی ویرایش کاربر"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanRemoveUsers"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 90,
                        Name = "CanRemoveUsers",
                        NormalizedName = "CanRemoveUsers".ToUpper(),
                        Description = "توانایی حذف کاربر"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanChangeUsersStatus"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 100,
                        Name = "CanChangeUsersStatus",
                        NormalizedName = "CanChangeUsersStatus".ToUpper(),
                        Description = "توانایی تغییر وضعیت کاربر"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanChangeUsersAccessLevel"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 110,
                        Name = "CanChangeUsersAccessLevel",
                        NormalizedName = "CanChangeUsersAccessLevel".ToUpper(),
                        Description = "توانایی تغییر سطح دسترسی کاربر"
                    }, default, false).Wait();
                }
            }
            #endregion

            #region ManageCategories
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!_repRoles.Get.Any(a => a.Name == "CanManageCategories"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "ManageCategoriesPage",
                        Sort = 120,
                        Name = "CanManageCategories",
                        NormalizedName = "CanManageCategories".ToUpper(),
                        Description = "توانایی مدیریت دسته بندی ها"
                    }, default, false).Wait();
                }
                else
                {
                    _Id = _repRoles.Get.Where(a => a.Name == "CanManageCategories").Select(a => a.Id).Single();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListCategories"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageCategoriesPage",
                        Sort = 130,
                        Name = "CanViewListCategories",
                        NormalizedName = "CanViewListCategories".ToUpper(),
                        Description = "توانایی مشاهده لیست دسته بندی ها"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanAddCategory"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageCategoriesPage",
                        Sort = 140,
                        Name = "CanAddCategory",
                        NormalizedName = "CanAddCategory".ToUpper(),
                        Description = "توانایی افزودن دسته بندی جدید"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanEditCategory"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageCategoriesPage",
                        Sort = 150,
                        Name = "CanEditCategory",
                        NormalizedName = "CanEditCategory".ToUpper(),
                        Description = "توانایی ویرایش دسته بندی"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanRemoveCategory"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageCategoriesPage",
                        Sort = 160,
                        Name = "CanRemoveCategory",
                        NormalizedName = "CanRemoveCategory".ToUpper(),
                        Description = "توانایی حذف دسته بندی"
                    }, default, false).Wait();
                }
            }
            #endregion

            #region ManageProducts
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!_repRoles.Get.Any(a => a.Name == "CanManageProducts"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "ManageProductPage",
                        Sort = 170,
                        Name = "CanManageProducts",
                        NormalizedName = "CanManageProducts".ToUpper(),
                        Description = "توانایی مدیریت محصولات"
                    }, default, false).Wait();
                }
                else
                {
                    _Id = _repRoles.Get.Where(a => a.Name == "CanManageProducts").Select(a => a.Id).Single();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListProducts"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 180,
                        Name = "CanViewListProducts",
                        NormalizedName = "CanViewListProducts".ToUpper(),
                        Description = "توانایی مشاهده لیست محصولات"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListAllUserProducts"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 190,
                        Name = "CanViewListAllUserProducts",
                        NormalizedName = "CanViewListAllUserProducts".ToUpper(),
                        Description = "توانایی مشاهده لیست محصولات همه ی کاربران"
                    }, default, false).Wait();
                }
            }
            #endregion

            _repRoles.SaveChangeAsync().Wait();

        }
    }
}