﻿using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
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
        BaseRepository<tblFileTypes> _FileTypes;
        public AddData_Countris()
        {
            _Countries = new BaseRepository<tblCountries>(new MainContext());
            _FileServer = new BaseRepository<tblFileServers>(new MainContext());
            _Language = new BaseRepository<tblLanguages>(new MainContext());
            _FileTypes = new BaseRepository<tblFileTypes>(new MainContext());

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
                    PhoneCode = "098",
                    tblFiles = new tblFiles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Title = "IranCountryFlag",
                        Date = DateTime.Now,
                        FileName = "IranCountryFlag.png",
                        FileServerId = _FileServer.GetNoTraking.Where(a => a.Name == "Public").Select(a => a.Id).Single(),
                        FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                        Path = "/Img/flags/",
                        SizeOnDisk = 0
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
                    PhoneCode = "001",
                    tblFiles = new tblFiles()
                    {
                        Id = new Guid().SequentialGuid(),
                        Title = "USACountryFlag",
                        Date = DateTime.Now,
                        FileName = "USACountryFlag.png",
                        FileServerId = _FileServer.GetNoTraking.Where(a => a.Name == "Public").Select(a => a.Id).Single(),
                        FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                        Path = "/Img/flags/",
                        SizeOnDisk = 0
                    }
                }, default).Wait();
            }
        }
    }
}
