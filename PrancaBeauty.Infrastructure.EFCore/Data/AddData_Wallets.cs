using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Wallets
    {
        BaseRepository<tblWallets> _RepWallets;
        BaseRepository<tblUsers> _RepUsers;
        BaseRepository<tblCurrencies> _RepCurrencies;
        public AddData_Wallets()
        {
            _RepWallets= new BaseRepository<tblWallets>(new MainContext());
            _RepUsers= new BaseRepository<tblUsers>(new MainContext());
            _RepCurrencies= new BaseRepository<tblCurrencies>(new MainContext());
        }

        public void Run()
        {
            var _UserId = _RepUsers.Get.Where(a => a.Email=="reza9025@gmail.com").Select(a => a.Id).Single();
            var RialCurrencyId = _RepCurrencies.Get.Where(a => a.Name=="Rial").Select(a => a.Id).Single();

            #region User Wallet
            {
                #region Encrypt WalletData
                string _JsonUserData = "";
                {
                    var JsonData = File.ReadAllText($@"{AssemblyDirectory}\Data\AddData_WalletUser.json", Encoding.UTF8);
                    _JsonUserData = JsonData.AesEncrypt(AuthConst.SecretKey);
                }
                #endregion

                if (!_RepWallets.Get.Where(a => a.UserId==_UserId).Any())
                {
                    _RepWallets.AddAsync(new tblWallets
                    {
                        Id= new Guid().SequentialGuid(),
                        Title="کیف پول ریالی",
                        UserId=_UserId,
                        CurrencyId=RialCurrencyId,
                        Data= _JsonUserData
                    }, default, true).Wait();
                }
            }
            #endregion

            #region Tax Wallet
            {
                #region Encrypt WalletData
                string _JsonTaxData = "";
                {
                    var JsonData = File.ReadAllText($@"{AssemblyDirectory}\Data\AddData_WalletTax.json", Encoding.UTF8);
                    _JsonTaxData = JsonData.AesEncrypt(AuthConst.SecretKey);
                }
                #endregion
                if (!_RepWallets.Get.Where(a => a.Title=="مالیات").Any())
                {
                    _RepWallets.AddAsync(new tblWallets
                    {
                        Id= new Guid().SequentialGuid(),
                        Title="مالیات",
                        UserId=null,
                        CurrencyId=RialCurrencyId,
                        Data= _JsonTaxData
                    }, default, true).Wait();
                }
            }
            #endregion

            #region Site Wallet
            {
                #region Encrypt WalletData
                string _JsonSiteData = "";
                {
                    var JsonData = File.ReadAllText($@"{AssemblyDirectory}\Data\AddData_WalletSite.json", Encoding.UTF8);
                    _JsonSiteData = JsonData.AesEncrypt(AuthConst.SecretKey);
                }
                #endregion

                if (!_RepWallets.Get.Where(a => a.Title=="حساب سایت").Any())
                {
                    _RepWallets.AddAsync(new tblWallets
                    {
                        Id= new Guid().SequentialGuid(),
                        Title="حساب سایت",
                        UserId=null,
                        CurrencyId=RialCurrencyId,
                        Data= _JsonSiteData
                    }, default, true).Wait();
                }
            }
            #endregion
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
