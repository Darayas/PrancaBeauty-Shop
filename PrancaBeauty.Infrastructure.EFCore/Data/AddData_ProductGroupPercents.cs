using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Entities;
using PrancaBeauty.Domin.Product.ProductGroupPercentAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_ProductGroupPercents
    {
        BaseRepository<tblProductGroupPercents> _RepProductGroupPercents;
        BaseRepository<tblProductGroups> _RepProductGroups;
        BaseRepository<tblUsers> _RepUsers;
        BaseRepository<tblWallets> _RepWallets;
        public AddData_ProductGroupPercents()
        {
            _RepProductGroupPercents= new BaseRepository<tblProductGroupPercents>(new MainContext());
            _RepProductGroups= new BaseRepository<tblProductGroups>(new MainContext());
            _RepWallets= new BaseRepository<tblWallets>(new MainContext());
            _RepUsers= new BaseRepository<tblUsers>(new MainContext());
        }

        public void Run()
        {

            var TaxWalletId = _RepWallets.Get.Where(a => a.UserId==null).Where(a => a.Title=="مالیات").Select(a => a.Id).Single();
            var SiteWalletId = _RepWallets.Get.Where(a => a.UserId==null).Where(a => a.Title=="حساب سایت").Select(a => a.Id).Single();

            #region SmartPhone
            {
                var _GroupId = _RepProductGroups.Get.Where(a => a.Name=="SmartPhone").Select(a => a.Id).Single();

                if (!_RepProductGroupPercents.Get.Where(a => a.WalletId==TaxWalletId).Where(a => a.ProductGroupId==_GroupId).Where(a => a.Title=="حساب مالیات").Any())
                {
                    _RepProductGroupPercents.AddAsync(new tblProductGroupPercents
                    {
                        Id= new Guid().SequentialGuid(),
                        ProductGroupId= _GroupId,
                        WalletId=TaxWalletId,
                        Title="حساب مالیات",
                        Percent=4
                    }, default, true).Wait();
                }

                if (!_RepProductGroupPercents.Get.Where(a => a.WalletId==SiteWalletId).Where(a => a.ProductGroupId==_GroupId).Where(a => a.Title=="حساب سایت").Any())
                {
                    _RepProductGroupPercents.AddAsync(new tblProductGroupPercents
                    {
                        Id= new Guid().SequentialGuid(),
                        ProductGroupId=_GroupId,
                        WalletId=SiteWalletId,
                        Title="حساب سایت",
                        Percent=7
                    }, default, true).Wait();
                }
            }
            #endregion

            #region IPhone13
            {
                var _GroupId = _RepProductGroups.Get.Where(a => a.Name=="IPhone13").Select(a => a.Id).Single();

                if (!_RepProductGroupPercents.Get.Where(a => a.WalletId==TaxWalletId).Where(a => a.ProductGroupId==_GroupId).Where(a => a.Title=="حساب مالیات").Any())
                {
                    _RepProductGroupPercents.AddAsync(new tblProductGroupPercents
                    {
                        Id= new Guid().SequentialGuid(),
                        ProductGroupId= _GroupId,
                        WalletId=TaxWalletId,
                        Title="حساب مالیات",
                        Percent=3
                    }, default, true).Wait();
                }

                if (!_RepProductGroupPercents.Get.Where(a => a.WalletId==SiteWalletId).Where(a => a.ProductGroupId==_GroupId).Where(a => a.Title=="حساب سایت").Any())
                {
                    _RepProductGroupPercents.AddAsync(new tblProductGroupPercents
                    {
                        Id= new Guid().SequentialGuid(),
                        ProductGroupId=_GroupId,
                        WalletId=SiteWalletId,
                        Title="حساب سایت",
                        Percent=10
                    }, default, true).Wait();
                }
            }
            #endregion

            #region Loptop
            {
                var _GroupId = _RepProductGroups.Get.Where(a => a.Name=="Loptop").Select(a => a.Id).Single();

                if (!_RepProductGroupPercents.Get.Where(a => a.WalletId==TaxWalletId).Where(a => a.ProductGroupId==_GroupId).Where(a => a.Title=="حساب مالیات").Any())
                {
                    _RepProductGroupPercents.AddAsync(new tblProductGroupPercents
                    {
                        Id= new Guid().SequentialGuid(),
                        ProductGroupId= _GroupId,
                        WalletId=TaxWalletId,
                        Title="حساب مالیات",
                        Percent=3
                    }, default, true).Wait();
                }

                if (!_RepProductGroupPercents.Get.Where(a => a.WalletId==SiteWalletId).Where(a => a.ProductGroupId==_GroupId).Where(a => a.Title=="حساب سایت").Any())
                {
                    _RepProductGroupPercents.AddAsync(new tblProductGroupPercents
                    {
                        Id= new Guid().SequentialGuid(),
                        ProductGroupId=_GroupId,
                        WalletId=SiteWalletId,
                        Title="حساب سایت",
                        Percent=8
                    }, default, true).Wait();
                }
            }
            #endregion
        }
    }
}
