using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.FileType
{
    public class FileTypeRepository : BaseRepository<tblFileTypes>, IFileTypeRepository
    {
        public FileTypeRepository(MainContext Context) : base(Context)
        {

        }
    }
}
