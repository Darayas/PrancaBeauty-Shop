using Framework.Common.ExMethods;
using Framework.Infrastructure;
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
    public class AddData_Countris
    {
        BaseRepository<tblCountries> _Countries;
        BaseRepository<tblFileServers> _FileServer;
        BaseRepository<tblLanguages> _Language;
        public AddData_Countris()
        {
            _Countries = new BaseRepository<tblCountries>(new MainContext());
            _FileServer = new BaseRepository<tblFileServers>(new MainContext());
            _Language = new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            if (!_Countries.Get.Any(a => a.Name == "Iran"))
            {
                _Countries.AddAsync(new tblCountries()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "Iran",
                    IsActive = true,
                    tblFiles = new tblFiles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Title = "IranCountryFlag",
                        Date = DateTime.Now,
                        FileName = "IranCountryFlag.png",
                        FileServerId = _FileServer.GetNoTraking.Where(a => a.Name == "Public").Select(a => a.Id).Single(),
                        MimeType = "image/png",
                        Path = "/Img/flags/",
                        SizeOnDisk = 0
                    },
                    tblCountries_Translates = new List<tblCountries_Translates> {
                        new tblCountries_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="ایران"
                        },
                        new tblCountries_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="Iran"
                        }
                    }
                }, default).Wait();
            }

            if (!_Countries.Get.Any(a => a.Name == "USA"))
            {
                _Countries.AddAsync(new tblCountries()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "USA",
                    IsActive = true,
                    tblFiles = new tblFiles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Title = "USACountryFlag",
                        Date = DateTime.Now,
                        FileName = "USACountryFlag.png",
                        FileServerId = _FileServer.GetNoTraking.Where(a => a.Name == "Public").Select(a => a.Id).Single(),
                        MimeType = "image/png",
                        Path = "/Img/flags/",
                        SizeOnDisk = 0
                    },
                    tblCountries_Translates = new List<tblCountries_Translates> {
                        new tblCountries_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="fa-IR").Select(a=>a.Id).Single(),
                            Title="امریکا"
                        },
                        new tblCountries_Translates{
                            Id= new Guid().SequentialGuid(),
                            LangId= _Language.Get.Where(a=>a.Code=="en-US").Select(a=>a.Id).Single(),
                            Title="USA"
                        }
                    }
                }, default).Wait();
            }
        }
    }
}
