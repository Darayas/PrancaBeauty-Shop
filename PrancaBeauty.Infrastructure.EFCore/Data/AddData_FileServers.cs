using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Common.ExMethods;
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
                    FtpData = "",
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
