using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
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
    public class AddData_Province
    {
        BaseRepository<tblCountries> _Countries;
        BaseRepository<tblProvinces> _Province;
        BaseRepository<tblLanguages> _Language;

        public AddData_Province()
        {
            _Countries = new BaseRepository<tblCountries>(new MainContext());
            _Province = new BaseRepository<tblProvinces>(new MainContext());
            _Language = new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            if (!_Province.Get.Any(a => a.Name == "Tehran"))
            {
                _Province.AddAsync(new tblProvinces()
                {
                    Id = new Guid().SequentialGuid(),
                    CountryId = _Countries.Get.Where(a => a.Name == "Iran").Select(a => a.Id).Single(),
                    Name = "Tehran",
                    IsActive = true,
                    tblProvinces_Translate = new List<tblProvinces_Translate> {
                        new tblProvinces_Translate{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="تهران"
                        },
                        new tblProvinces_Translate{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Tehran"
                        }
                    }
                }, default).Wait();
            }

            if (!_Province.Get.Any(a => a.Name == "Esfahan"))
            {
                _Province.AddAsync(new tblProvinces()
                {
                    Id = new Guid().SequentialGuid(),
                    CountryId = _Countries.Get.Where(a => a.Name == "Iran").Select(a => a.Id).Single(),
                    Name = "Esfahan",
                    IsActive = true,
                    tblProvinces_Translate = new List<tblProvinces_Translate> {
                        new tblProvinces_Translate{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="اصفهان"
                        },
                        new tblProvinces_Translate{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Esfahan"
                        }
                    }
                }, default).Wait();
            }

            if (!_Province.Get.Any(a => a.Name == "Fars"))
            {
                _Province.AddAsync(new tblProvinces()
                {
                    Id = new Guid().SequentialGuid(),
                    CountryId = _Countries.Get.Where(a => a.Name == "Iran").Select(a => a.Id).Single(),
                    Name = "Fars",
                    IsActive = true,
                    tblProvinces_Translate = new List<tblProvinces_Translate> {
                        new tblProvinces_Translate{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="فارس"
                        },
                        new tblProvinces_Translate{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Fars"
                        }
                    }
                }, default).Wait();
            }
        }
    }
}
