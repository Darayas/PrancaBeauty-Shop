using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Currencies
    {
        BaseRepository<tblCurrencies> _Currencies;
        BaseRepository<tblCountries> _Countries;
        BaseRepository<tblLanguages> _Languages;
        public AddData_Currencies()
        {
            _Currencies = new BaseRepository<tblCurrencies>(new MainContext());
            _Countries = new BaseRepository<tblCountries>(new MainContext());
            _Languages = new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            if (!_Currencies.Get.Where(a => a.tblCountry.Name == "Iran").Where(a => a.Name == "Rial").Any())
            {
                _Currencies.AddAsync(new tblCurrencies()
                {
                    Id = new Guid().SequentialGuid(),
                    CountryId = _Countries.Get.Where(a => a.Name == "Iran").Select(a => a.Id).Single(),
                    Name = "Rial",
                    IsDefault = true,
                    aDec = ".",
                    aSep = ",",
                    mDec = "0",
                    Symbol = "ريال",
                    vMax = "99999999",
                    tblCurrency_Translates = new List<tblCurrency_Translates>() {
                        new tblCurrency_Translates(){
                            Id=new Guid().SequentialGuid(),
                            LangId= _Languages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="ریال"
                        },
                        new tblCurrency_Translates(){
                            Id=new Guid().SequentialGuid(),
                            LangId= _Languages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Rial"
                        }
                    }
                }, default, false).Wait();
            }

            if (!_Currencies.Get.Where(a => a.tblCountry.Name == "USA").Where(a => a.Name == "Dollars").Any())
            {
                _Currencies.AddAsync(new tblCurrencies()
                {
                    Id = new Guid().SequentialGuid(),
                    CountryId = _Countries.Get.Where(a => a.Name == "USA").Select(a => a.Id).Single(),
                    Name = "Dollars",
                    IsDefault = true,
                    aDec = ".",
                    aSep = ",",
                    mDec = "2",
                    Symbol = "$",
                    vMax = "999999",
                    tblCurrency_Translates = new List<tblCurrency_Translates>() {
                        new tblCurrency_Translates(){
                            Id=new Guid().SequentialGuid(),
                            LangId= _Languages.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="دلار"
                        },
                        new tblCurrency_Translates(){
                            Id=new Guid().SequentialGuid(),
                            LangId= _Languages.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Dollars"
                        }
                    }
                }, default, false).Wait();
            }

            _Currencies.SaveChangeAsync().Wait();

        }
    }
}
