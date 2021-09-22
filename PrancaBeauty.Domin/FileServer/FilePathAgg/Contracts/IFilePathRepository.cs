using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.FileServer.FilePathAgg.Contracts
{
    public interface IFilePathRepository : IRepository<tblFilePaths>
    {
    }
}
