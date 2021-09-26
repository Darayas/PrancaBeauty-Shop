using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_FileType
    {
        BaseRepository<tblFileTypes> _FileType;
        public AddData_FileType()
        {
            _FileType = new BaseRepository<tblFileTypes>(new MainContext());
        }

        public void Run()
        {
            if (!_FileType.Get.Where(a => a.MimeType == "image/png").Any())
            {
                _FileType.AddAsync(new tblFileTypes()
                {
                    Id = new Guid().SequentialGuid(),
                    IconUrl = "/shared/img/filetype/png.png",
                    MimeType = "image/png",
                    Extentions = "png"
                }, default, false).Wait();
            }

            if (!_FileType.Get.Where(a => a.MimeType == "image/jpg").Any())
            {
                _FileType.AddAsync(new tblFileTypes()
                {
                    Id = new Guid().SequentialGuid(),
                    IconUrl = "/shared/img/filetype/jpg.png",
                    MimeType = "image/jpg",
                    Extentions = "jpg"
                }, default, false).Wait();
            }

            if (!_FileType.Get.Where(a => a.MimeType == "image/jpeg").Any())
            {
                _FileType.AddAsync(new tblFileTypes()
                {
                    Id = new Guid().SequentialGuid(),
                    IconUrl = "/shared/img/filetype/jpg.png",
                    MimeType = "image/jpeg",
                    Extentions = "jpg"
                }, default, false).Wait();
            }

            if (!_FileType.Get.Where(a => a.MimeType == "video/mp4").Any())
            {
                _FileType.AddAsync(new tblFileTypes()
                {
                    Id = new Guid().SequentialGuid(),
                    IconUrl = "/shared/img/filetype/mp4.png",
                    MimeType = "video/mp4",
                    Extentions = "mp4"
                }, default, false).Wait();
            }

            _FileType.SaveChangeAsync().Wait();
        }
    }
}
