using Framework.Common.ExMethods;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
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
    public class AddData_languages
    {
        BaseRepository<tblLanguages> _repLang;
        BaseRepository<tblFileServers> _FileServer;
        BaseRepository<tblCountries> _Country;
        public AddData_languages()
        {
            _repLang = new BaseRepository<tblLanguages>(new MainContext());
            _FileServer = new BaseRepository<tblFileServers>(new MainContext());
            _Country = new BaseRepository<tblCountries>(new MainContext());
        }

        public void Run()
        {
            if (!_repLang.GetNoTraking.Any(a => a.Code == "fa-IR"))
            {
                _repLang.AddAsync(new tblLanguages()
                {
                    Id = new Guid().SequentialGuid(),
                    Code = "fa-IR",
                    IsActive = true,
                    IsRtl = true,
                    Name = "Persian_IR",
                    NativeName = "فارسی (ایران)",
                    Abbr = "fa",
                    UseForSiteLanguage = true,
                    CountryId = _Country.Get.Where(a => a.Name == "Iran").Select(a => a.Id).Single()
                }, default, true).Wait();
            }

            if (!_repLang.GetNoTraking.Any(a => a.Code == "en-US"))
            {
                _repLang.AddAsync(new tblLanguages()
                {
                    Id = new Guid().SequentialGuid(),
                    Code = "en-US",
                    IsActive = true,
                    IsRtl = false,
                    Name = "English_USA",
                    NativeName = "English (USA)",
                    Abbr = "en",
                    UseForSiteLanguage = true,
                    CountryId = _Country.Get.Where(a => a.Name == "USA").Select(a => a.Id).Single()
                }, default, true).Wait();
            }
        }
    }
}
