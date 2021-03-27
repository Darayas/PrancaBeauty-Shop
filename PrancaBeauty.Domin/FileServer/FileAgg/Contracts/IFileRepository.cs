using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.FileServer.FileAgg.Contracts
{
    public interface IFileRepository : IRepository<tblFiles>
    {
    }
}
