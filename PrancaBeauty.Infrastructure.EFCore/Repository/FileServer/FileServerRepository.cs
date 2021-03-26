using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.ServerAgg.Contracts;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.FileServer
{
    public class FileServerRepository : BaseRepository<tblFileServers>, IFileServerRepository
    {
        public FileServerRepository(MainContext context) : base(context)
        {

        }
    }
}
