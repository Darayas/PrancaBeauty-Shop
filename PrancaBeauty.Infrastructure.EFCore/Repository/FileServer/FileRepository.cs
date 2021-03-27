using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FileAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.FileServer
{
    public class FileRepository : BaseRepository<tblFiles>, IFileRepository
    {
        public FileRepository(MainContext context) : base(context)
        {

        }
    }
}
