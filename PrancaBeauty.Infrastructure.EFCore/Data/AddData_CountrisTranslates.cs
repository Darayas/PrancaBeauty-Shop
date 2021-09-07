using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_CountrisTranslates
    {
        BaseRepository<tblCountries> _Countries;
        BaseRepository<tblLanguages> _Language;
        BaseRepository<tblCountries_Translates> _Countries_Translates;
        public AddData_CountrisTranslates()
        {
            var Context = new MainContext();

            _Countries = new BaseRepository<tblCountries>(Context);
            _Language = new BaseRepository<tblLanguages>(Context);
            _Countries_Translates = new BaseRepository<tblCountries_Translates>(Context);
        }

        public void Run()
        {

            // ایران
            if (!_Countries_Translates.Get.Where(a => a.CountryId == _Countries.Get.Where(b => b.Name == "Iran").Select(b => b.Id).Single()).Where(a => a.LangId == _Language.Get.Where(b => b.Code == "fa-IR").Select(b => b.Id).Single()).Any())
            {
                _Countries_Translates.AddAsync(new tblCountries_Translates()
                {
                    Id=new Guid().SequentialGuid(),
                    CountryId= _Countries.Get.Where(b => b.Name == "Iran").Select(b => b.Id).Single(),
                    LangId = _Language.Get.Where(b => b.Code == "fa-IR").Select(b => b.Id).Single(),
                    Title="ایران"
                }, default).Wait();
            }

            // Iran
            if (!_Countries_Translates.Get.Where(a => a.CountryId == _Countries.Get.Where(b => b.Name == "Iran").Select(b => b.Id).Single()).Where(a => a.LangId == _Language.Get.Where(b => b.Code == "en-US").Select(b => b.Id).Single()).Any())
            {
                _Countries_Translates.AddAsync(new tblCountries_Translates()
                {
                    Id = new Guid().SequentialGuid(),
                    CountryId = _Countries.Get.Where(b => b.Name == "Iran").Select(b => b.Id).Single(),
                    LangId = _Language.Get.Where(b => b.Code == "en-US").Select(b => b.Id).Single(),
                    Title = "Iran"
                }, default).Wait();
            }

            // امریکا
            if (!_Countries_Translates.Get.Where(a => a.CountryId == _Countries.Get.Where(b => b.Name == "USA").Select(b => b.Id).Single()).Where(a => a.LangId == _Language.Get.Where(b => b.Code == "fa-IR").Select(b => b.Id).Single()).Any())
            {
                _Countries_Translates.AddAsync(new tblCountries_Translates()
                {
                    Id = new Guid().SequentialGuid(),
                    CountryId = _Countries.Get.Where(b => b.Name == "USA").Select(b => b.Id).Single(),
                    LangId = _Language.Get.Where(b => b.Code == "fa-IR").Select(b => b.Id).Single(),
                    Title = "امریکا"
                }, default).Wait();
            }

            // USA
            if (!_Countries_Translates.Get.Where(a => a.CountryId == _Countries.Get.Where(b => b.Name == "USA").Select(b => b.Id).Single()).Where(a => a.LangId == _Language.Get.Where(b => b.Code == "en-US").Select(b => b.Id).Single()).Any())
            {
                _Countries_Translates.AddAsync(new tblCountries_Translates()
                {
                    Id = new Guid().SequentialGuid(),
                    CountryId = _Countries.Get.Where(b => b.Name == "USA").Select(b => b.Id).Single(),
                    LangId = _Language.Get.Where(b => b.Code == "en-US").Select(b => b.Id).Single(),
                    Title = "USA"
                }, default).Wait();
            }
        }
    }
}
