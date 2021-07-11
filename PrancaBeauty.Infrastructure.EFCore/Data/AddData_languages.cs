using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
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
        public AddData_languages()
        {
            _repLang = new BaseRepository<tblLanguages>(new MainContext());
            _FileServer = new BaseRepository<tblFileServers>(new MainContext());
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
                    tblFile = new tblFiles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Title = "IranFlag",
                        Date = DateTime.Now,
                        FileName = "IranFlag.png",
                        FileServerId = _FileServer.GetNoTraking.Where(a => a.Name == "Public").Select(a => a.Id).Single(),
                        MimeType = "image/png",
                        Path = "/Img/flags/",
                        SizeOnDisk = 0
                    }
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
                    tblFile = new tblFiles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Title = "UsFlag",
                        Date = DateTime.Now,
                        FileName = "UsFlag.png",
                        FileServerId = _FileServer.GetNoTraking.Where(a => a.Name == "Public").Select(a => a.Id).Single(),
                        MimeType = "image/png",
                        Path = "/Img/flags/",
                        SizeOnDisk = 0
                    }
                }, default, true).Wait();
            }
        }
    }
}
