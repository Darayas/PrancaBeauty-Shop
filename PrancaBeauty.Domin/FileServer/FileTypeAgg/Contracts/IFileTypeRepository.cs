using Framework.Domain.Contracts;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.FileServer.FileTypeAgg.Contracts
{
    public interface IFileTypeRepository : IRepository<tblFileTypes>
    {
    }
}
