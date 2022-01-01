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

                if (!_repRoles.Get.Any(a => a.Name == "CanChangeProfileImage"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageUsersPage",
                        Sort = 115,
                        Name = "CanChangeProfileImage",
                        NormalizedName = "CanChangeProfileImage".ToUpper(),
                        Description = "توانایی تغییر تصویر پروفایل"
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

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListAllAuthorUserProducts"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 185,
                        Name = "CanViewListAllAuthorUserProducts",
                        NormalizedName = "CanViewListAllAuthorUserProducts".ToUpper(),
                        Description = "توانایی مشاهده لیست محصولات همه ی نویسندگان"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListAllSellerUserProducts"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 190,
                        Name = "CanViewListAllSellerUserProducts",
                        NormalizedName = "CanViewListAllSellerUserProducts".ToUpper(),
                        Description = "توانایی مشاهده لیست محصولات همه ی فروشندگان"
                    }, default, false).Wait();
                }


                if (!_repRoles.Get.Any(a => a.Name == "CanAddProduct"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 200,
                        Name = "CanAddProduct",
                        NormalizedName = "CanAddProduct".ToUpper(),
                        Description = "توانایی افزودن محصول جدید"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanEditProduct"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 210,
                        Name = "CanEditProduct",
                        NormalizedName = "CanEditProduct".ToUpper(),
                        Description = "توانایی ویرایش محصول"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanEditProductForAllUser"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 211,
                        Name = "CanEditProductForAllUser",
                        NormalizedName = "CanEditProductForAllUser".ToUpper(),
                        Description = "توانایی ویرایش محصولات دیگر کاربران"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanRemoveAllUserProduct"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 215,
                        Name = "CanRemoveAllUserProduct",
                        NormalizedName = "CanRemoveAllUserProduct".ToUpper(),
                        Description = "توانایی حذف محصولات دیگر کاربران"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanRemoveProduct"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 220,
                        Name = "CanRemoveProduct",
                        NormalizedName = "CanRemoveProduct".ToUpper(),
                        Description = "توانایی حذف محصول"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanMoveToRecycleBinAllUserProduct"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 225,
                        Name = "CanMoveToRecycleBinAllUserProduct",
                        NormalizedName = "CanMoveToRecycleBinAllUserProduct".ToUpper(),
                        Description = "توانایی انتقال محصولات دیگر کاربران به زباله دان"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanMoveToRecycleBinProduct"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductsPage",
                        Sort = 230,
                        Name = "CanMoveToRecycleBinProduct",
                        NormalizedName = "CanMoveToRecycleBinProduct".ToUpper(),
                        Description = "توانایی انتقال محصول به زباله دان"
                    }, default, false).Wait();
                }
            }
            #endregion

            #region ManageProductSeller
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!_repRoles.Get.Any(a => a.Name == "CanViewListProductSellerList"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "ManageProductSellerListPage",
                        Sort = 240,
                        Name = "CanViewListProductSellerList",
                        NormalizedName = "CanViewListProductSellerList".ToUpper(),
                        Description = "توانایی مدیریت فروشندگان محصول"
                    }, default, false).Wait();
                }
                else
                {
                    _Id = _repRoles.Get.Where(a => a.Name == "CanManageProducts").Select(a => a.Id).Single();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListProductSellerListAllUser"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductSellerListPage",
                        Sort = 245,
                        Name = "CanViewListProductSellerListAllUser",
                        NormalizedName = "CanViewListProductSellerListAllUser".ToUpper(),
                        Description = "توانایی مدیریت همه ی فروشندگان محصول"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanAddProductSeller"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductSellerListPage",
                        Sort = 250,
                        Name = "CanAddProductSeller",
                        NormalizedName = "CanAddProductSeller".ToUpper(),
                        Description = "توانایی افزودن فروشنده به محصول"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanAddProductSellerAllUser"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductSellerListPage",
                        Sort = 255,
                        Name = "CanAddProductSellerAllUser",
                        NormalizedName = "CanAddProductSellerAllUser".ToUpper(),
                        Description = "توانایی افزودن فروشنده به محصول برای همه کاربران"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanEditProductSeller"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductSellerListPage",
                        Sort = 260,
                        Name = "CanEditProductSeller",
                        NormalizedName = "CanEditProductSeller".ToUpper(),
                        Description = "توانایی ویرایش فروشنده ی محصول"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanDeleteProductSeller"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageProductSellerListPage",
                        Sort = 270,
                        Name = "CanDeleteProductSeller",
                        NormalizedName = "CanDeleteProductSeller".ToUpper(),
                        Description = "توانایی حذف فروشنده ی محصول"
                    }, default, false).Wait();
                }
            }
            #endregion

            #region ManageFiles
            {
                Guid _Id = new Guid().SequentialGuid();
                if (!_repRoles.Get.Any(a => a.Name == "CanManageFiles"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = _Id,
                        ParentId = null,
                        PageName = "ManageFilePage",
                        Sort = 350,
                        Name = "CanManageFiles",
                        NormalizedName = "CanManageFiles".ToUpper(),
                        Description = "توانایی مدیریت فایل ها"
                    }, default, false).Wait();
                }
                else
                {
                    _Id = _repRoles.Get.Where(a => a.Name == "CanManageFiles").Select(a => a.Id).Single();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanViewListFiles"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageFilePage",
                        Sort = 360,
                        Name = "CanViewListFiles",
                        NormalizedName = "CanViewListFiles".ToUpper(),
                        Description = "توانایی مشاهده لیست فایل ها"
                    }, default, false).Wait();
                }

                if (!_repRoles.Get.Any(a => a.Name == "CanManageAllUserFiles"))
                {
                    _repRoles.AddAsync(new tblRoles()
                    {
                        Id = new Guid().SequentialGuid(),
                        ParentId = _Id,
                        PageName = "ManageFilePage",
                        Sort = 370,
                        Name = "CanManageAllUserFiles",
                        NormalizedName = "CanManageAllUserFiles".ToUpper(),
                        Description = "توانایی مدیریت لیست فایل های دیگر کاربران"
                    }, default, false).Wait();
                }
            }
            #endregion

            _repRoles.SaveChangeAsync().Wait();

        }
    }
}