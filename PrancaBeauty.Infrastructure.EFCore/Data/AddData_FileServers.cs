using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                #region Encrypt FtpData
                string _FtpData = "";
                {
                    string JsonData = File.ReadAllText($@"{AssemblyDirectory}\Data\AddData_FileServers.json", Encoding.UTF8);
                    _FtpData = JsonData.AesEncrypt(AuthConst.SecretKey);
                }
                #endregion

                _repFileServer.AddAsync(new tblFileServers()
                {
                    Id = new Guid().SequentialGuid(),
                    Name = "Public",
                    FtpData = _FtpData,
                    Capacity = 10737418240,
                    Description = "",
                    HttpDomin = "http://127.0.0.111",
                    HttpPath = "/Main",
                    IsActive = true,
                }, default, false).Wait();
            }

            _repFileServer.SaveChangeAsync().Wait();
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
