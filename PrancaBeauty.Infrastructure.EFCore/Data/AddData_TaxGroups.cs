using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Bills.TaxAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Linq;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_TaxGroups
    {
        BaseRepository<tblTaxGroups> _RepTaxGroup;
        BaseRepository<tblCountries> _RepCountry;
        public AddData_TaxGroups()
        {
            _RepTaxGroup= new BaseRepository<tblTaxGroups>(new MainContext());
            _RepCountry= new BaseRepository<tblCountries>(new MainContext());
        }

        public void Run()
        {
            var _IranCountryId = _RepCountry.Get.Where(b => b.Name=="Iran").Select(b => b.Id).Single();
            var _USACountryId = _RepCountry.Get.Where(b => b.Name=="USA").Select(b => b.Id).Single();

            #region Iran - لوازم آرایشی و بهداشتی
            {
                if (!_RepTaxGroup.Get.Where(a => a.CountryId==_IranCountryId).Where(a => a.Title=="لوازم آرایشی و بهداشتی").Any())
                {
                    _RepTaxGroup.AddAsync(new tblTaxGroups
                    {
                        Id= new Guid().SequentialGuid(),
                        CountryId= _IranCountryId,
                        Title="لوازم آرایشی و بهداشتی",
                        Percent=16.5,
                        Description=""
                    }, default, true).Wait();
                }
            }
            #endregion

            #region Iran - گوشی موبایل هوشمند
            {
                if (!_RepTaxGroup.Get.Where(a => a.CountryId==_IranCountryId).Where(a => a.Title=="گوشی موبایل هوشمند").Any())
                {
                    _RepTaxGroup.AddAsync(new tblTaxGroups
                    {
                        Id= new Guid().SequentialGuid(),
                        CountryId= _IranCountryId,
                        Title="گوشی موبایل هوشمند",
                        Percent=9,
                        Description=""
                    }, default, true).Wait();
                }
            }
            #endregion

            #region Iran - لوازم گوشی موبایل
            {
                if (!_RepTaxGroup.Get.Where(a => a.CountryId==_IranCountryId).Where(a => a.Title=="لوازم گوشی موبایل").Any())
                {
                    _RepTaxGroup.AddAsync(new tblTaxGroups
                    {
                        Id= new Guid().SequentialGuid(),
                        CountryId= _IranCountryId,
                        Title="لوازم گوشی موبایل",
                        Percent=9,
                        Description=""
                    }, default, true).Wait();
                }
            }
            #endregion

            #region Iran - کامپیوتر و لپتاپ
            {
                if (!_RepTaxGroup.Get.Where(a => a.CountryId==_IranCountryId).Where(a => a.Title=="کامپیوتر و لپتاپ").Any())
                {
                    _RepTaxGroup.AddAsync(new tblTaxGroups
                    {
                        Id= new Guid().SequentialGuid(),
                        CountryId= _IranCountryId,
                        Title="کامپیوتر و لپتاپ",
                        Percent=11,
                        Description=""
                    }, default, true).Wait();
                }
            }
            #endregion

            #region USA - cosmetics
            {
                if (!_RepTaxGroup.Get.Where(a => a.CountryId==_USACountryId).Where(a => a.Title=="cosmetics").Any())
                {
                    _RepTaxGroup.AddAsync(new tblTaxGroups
                    {
                        Id= new Guid().SequentialGuid(),
                        CountryId= _USACountryId,
                        Title="cosmetics",
                        Percent=16.5,
                        Description=""
                    }, default, true).Wait();
                }
            }
            #endregion

            #region USA - Smartphone
            {
                if (!_RepTaxGroup.Get.Where(a => a.CountryId==_USACountryId).Where(a => a.Title=="Smartphone").Any())
                {
                    _RepTaxGroup.AddAsync(new tblTaxGroups
                    {
                        Id= new Guid().SequentialGuid(),
                        CountryId= _USACountryId,
                        Title="Smartphone",
                        Percent=9,
                        Description=""
                    }, default, true).Wait();
                }
            }
            #endregion

            #region USA -Mobile phone accessories
            {
                if (!_RepTaxGroup.Get.Where(a => a.CountryId==_USACountryId).Where(a => a.Title=="Mobile phone accessories").Any())
                {
                    _RepTaxGroup.AddAsync(new tblTaxGroups
                    {
                        Id= new Guid().SequentialGuid(),
                        CountryId= _USACountryId,
                        Title="Mobile phone accessories",
                        Percent=9,
                        Description=""
                    }, default, true).Wait();
                }
            }
            #endregion

            #region USA - Computer and laptop
            {
                if (!_RepTaxGroup.Get.Where(a => a.CountryId==_USACountryId).Where(a => a.Title=="Computer and laptop").Any())
                {
                    _RepTaxGroup.AddAsync(new tblTaxGroups
                    {
                        Id= new Guid().SequentialGuid(),
                        CountryId= _USACountryId,
                        Title="Computer and laptop",
                        Percent=11,
                        Description=""
                    }, default, true).Wait();
                }
            }
            #endregion
        }
    }
}
