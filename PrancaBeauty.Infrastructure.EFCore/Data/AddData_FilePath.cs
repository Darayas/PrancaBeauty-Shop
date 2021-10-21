using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_FilePath
    {
        BaseRepository<tblFilePaths> _FilePath;
        BaseRepository<tblFileServers> _FileServers;
        public AddData_FilePath()
        {
            _FilePath = new BaseRepository<tblFilePaths>(new MainContext());
            _FileServers = new BaseRepository<tblFileServers>(new MainContext());

        }

        public void Run()
        {
            if (!_FilePath.Get.Where(a => a.tblFileServer.Name == "Public").Where(a => a.Path == "/Img/flags/").Any())
            {
                _FilePath.AddAsync(new tblFilePaths()
                {
                    Id = new Guid().SequentialGuid(),
                    FileServerId = _FileServers.Get.Where(a => a.Name == "Public").Select(a => a.Id).Single(),
                    Path = "/Img/flags/"
                }, default, false).Wait();
            }

            if (!_FilePath.Get.Where(a => a.tblFileServer.Name == "Public").Where(a => a.Path == "/image/png/2021/1/1/").Any())
            {
                _FilePath.AddAsync(new tblFilePaths()
                {
                    Id = new Guid().SequentialGuid(),
                    FileServerId = _FileServers.Get.Where(a => a.Name == "Public").Select(a => a.Id).Single(),
                    Path = "/image/png/2021/1/1/"
                }, default, false).Wait();
            }

            _FilePath.SaveChangeAsync().Wait();
        }
    }
}
