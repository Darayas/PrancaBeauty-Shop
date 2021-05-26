using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.CityAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.Region.ProvinceAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_Cities
    {
        BaseRepository<tblCities> _Cities;
        BaseRepository<tblProvinces> _Province;
        BaseRepository<tblLanguages> _Language;

        public AddData_Cities()
        {
            _Cities = new BaseRepository<tblCities>(new MainContext());
            _Province = new BaseRepository<tblProvinces>(new MainContext());
            _Language = new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            if (!_Cities.Get.Any(a => a.Name == "Tehran"))
            {
                _Cities.AddAsync(new tblCities()
                {
                    Id = new Guid().SequentialGuid(),
                    ProvinceId = _Province.Get.Where(a => a.Name == "Tehran").Select(a => a.Id).Single(),
                    Name = "Tehran",
                    IsActive = true,
                    tblCities_Translates = new List<tblCities_Translates> {
                        new tblCities_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="تهران"
                        },
                        new tblCities_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Tehran"
                        }
                    }
                }, default).Wait();
            }

            if (!_Cities.Get.Any(a => a.Name == "Shiraz"))
            {
                _Cities.AddAsync(new tblCities()
                {
                    Id = new Guid().SequentialGuid(),
                    ProvinceId = _Province.Get.Where(a => a.Name == "Fars").Select(a => a.Id).Single(),
                    Name = "Shiraz",
                    IsActive = true,
                    tblCities_Translates = new List<tblCities_Translates> {
                        new tblCities_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="شیراز"
                        },
                        new tblCities_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Shiraz"
                        }
                    }
                }, default).Wait();
            }

            if (!_Cities.Get.Any(a => a.Name == "Esfahan"))
            {
                _Cities.AddAsync(new tblCities()
                {
                    Id = new Guid().SequentialGuid(),
                    ProvinceId = _Province.Get.Where(a => a.Name == "Esfahan").Select(a => a.Id).Single(),
                    Name = "Esfahan",
                    IsActive = true,
                    tblCities_Translates = new List<tblCities_Translates> {
                        new tblCities_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="اصفهان"
                        },
                        new tblCities_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Esfahan"
                        }
                    }
                }, default).Wait();
            }
        }
    }
}
