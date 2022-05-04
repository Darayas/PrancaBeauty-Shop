using Framework.Domain.Contracts;
using PrancaBeauty.Domin.FileServer.ServerAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.FileServer.ServerAgg.Contracts
{
    public interface IFileServerRepository : IRepository<tblFileServers>
    {
    }
}
