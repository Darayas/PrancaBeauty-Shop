using PrancaBeauty.Application.Contracts.FileServer;
using PrancaBeauty.Domin.FileServer.ServerAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.FileServer
{
    public class FileServerApplication : IFileServerApplication
    {
        private readonly IFileServerRepository _FileServerRepository;

        public FileServerApplication(IFileServerRepository fileServerRepository)
        {
            _FileServerRepository = fileServerRepository;
        }

        public async Task<OutGetServerDetails> GetServerDetailsAsync(string ServerName)
        {

        }
    }
}
