using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
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
        BaseRepository<tblLanguages> _Language;
        BaseRepository<tblFileTypes> _FileTypes;
        BaseRepository<tblFilePaths> _FilePaths;
        public AddData_Countris()
        {
            _Countries = new BaseRepository<tblCountries>(new MainContext());
            _Language = new BaseRepository<tblLanguages>(new MainContext());
            _FileTypes = new BaseRepository<tblFileTypes>(new MainContext());
            _FilePaths = new BaseRepository<tblFilePaths>(new MainContext());
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
                        FilePathId = _FilePaths.Get.Where(a => a.Path == "/image/png/2021/1/1/").Where(a => a.tblFileServer.Name == "Public").Select(a => a.Id).Single(),
                        Title = "IranCountryFlag",
                        Date = DateTime.Now,
                        FileName = "IranCountryFlag.png",
                        FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
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
                        FilePathId = _FilePaths.Get.Where(a => a.Path == "/image/png/2021/1/1/").Where(a => a.tblFileServer.Name == "Public").Select(a => a.Id).Single(),
                        Title = "USACountryFlag",
                        Date = DateTime.Now,
                        FileName = "USACountryFlag.png",
                        FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                        SizeOnDisk = 0
                    }
                }, default).Wait();
            }
        }
    }
}
