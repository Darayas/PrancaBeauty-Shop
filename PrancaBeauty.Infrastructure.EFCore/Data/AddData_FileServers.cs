using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Data
{
    public class AddData_FileServers
    {
        BaseRepository<tblFileServers> _repFileServer;
        public AddData_FileServers()
        {
            _repFileServer = new BaseRepository<tblFileServers>(new MainContext());
        }

        public void Run()
        {
            if (!_repFileServer.Get.Any(a => a.Name == "Public"))
            {
                _repFileServer.AddAsync(new tblFileServers()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "Public",
                    FtpData = "0O9m6iLTbav1UXlihSeD5uq6LEZEIwDs87i4I2gMhrPCeLBEd4OH9fGJ93AbUpIgWtcpOTiGo0LVGUX9T5WZ2S/VNaQFfQjEWNORjEUMHuYk8yob5KakMFh4iwfDYCEu",
                    Capacity = 0,
                    Description = "",
                    HttpDomin = "http://127.0.0.111",
                    HttpPath = "/Main",
                    IsActive = true,
                }, default, false).Wait();
            }

            _repFileServer.SaveChangeAsync().Wait();
        }
    }
}
