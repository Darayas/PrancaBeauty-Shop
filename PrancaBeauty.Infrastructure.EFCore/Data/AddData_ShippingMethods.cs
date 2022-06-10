using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_ShippingMethods
    {
        BaseRepository<tblShippingMethods> _RepShippingMethod;
        BaseRepository<tblFiles> _RepFile;
        BaseRepository<tblLanguages> _RepLang;
        BaseRepository<tblFileTypes> _FileTypes;
        BaseRepository<tblFilePaths> _FilePaths;
        public AddData_ShippingMethods()
        {
            _RepShippingMethod= new BaseRepository<tblShippingMethods>(new MainContext());
            _RepFile= new BaseRepository<tblFiles>(new MainContext());
            _FileTypes= new BaseRepository<tblFileTypes>(new MainContext());
            _FilePaths= new BaseRepository<tblFilePaths>(new MainContext());
            _RepLang= new BaseRepository<tblLanguages>(new MainContext());
        }

        public void Run()
        {
            var _PersianLangId = _RepLang.Get.Where(a => a.Code=="fa-IR").Select(a => a.Id).Single();
            var _EnLangId = _RepLang.Get.Where(a => a.Code=="en-US").Select(a => a.Id).Single();

            if (!_RepShippingMethod.Get.Where(a => a.Name=="IranPostService").Any())
            {
                _RepShippingMethod.AddAsync(new tblShippingMethods
                {
                    Id=new Guid().SequentialGuid(),
                    Name="IranPostService",
                    tblFiles= new tblFiles
                    {
                        Id = new Guid().SequentialGuid(),
                        FilePathId = _FilePaths.Get.Where(a => a.Path == "/image/png/2022/1/1/").Where(a => a.tblFileServer.Name == "Public").Select(a => a.Id).Single(),
                        Title = "IranPostService",
                        Date = DateTime.Now,
                        FileName = "IranPostService.png",
                        FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                        SizeOnDisk = 0
                    },
                    tblShippingMethodTranslate= new List<tblShippingMethodTranslate>
                    {
                        new tblShippingMethodTranslate{
                            Id = new Guid().SequentialGuid(),
                            LangId=_PersianLangId,
                            Title="شرکت پست ایران"
                        },
                        new tblShippingMethodTranslate{
                            Id = new Guid().SequentialGuid(),
                            LangId=_EnLangId,
                            Title="IranPostService"
                        }
                    }
                }, default, true).Wait();
            }

            if (!_RepShippingMethod.Get.Where(a => a.Name=="Tipax").Any())
            {
                _RepShippingMethod.AddAsync(new tblShippingMethods
                {
                    Id=new Guid().SequentialGuid(),
                    Name="Tipax",
                    tblFiles= new tblFiles
                    {
                        Id = new Guid().SequentialGuid(),
                        FilePathId = _FilePaths.Get.Where(a => a.Path == "/image/png/2022/1/1/").Where(a => a.tblFileServer.Name == "Public").Select(a => a.Id).Single(),
                        Title = "Tipax",
                        Date = DateTime.Now,
                        FileName = "Tipax.png",
                        FileTypeId = _FileTypes.Get.Where(a => a.MimeType == "image/png").Select(a => a.Id).Single(),
                        SizeOnDisk = 0
                    },
                    tblShippingMethodTranslate= new List<tblShippingMethodTranslate>
                    {
                        new tblShippingMethodTranslate{
                            Id = new Guid().SequentialGuid(),
                            LangId=_PersianLangId,
                            Title="تیپاکس"
                        },
                        new tblShippingMethodTranslate{
                            Id = new Guid().SequentialGuid(),
                            LangId=_EnLangId,
                            Title="Tipax"
                        }
                    }
                }, default, true).Wait();
            }
        }
    }
}
