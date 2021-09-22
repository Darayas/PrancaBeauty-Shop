using Framework.Infrastructure;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.FilePath
{
    public class FilePathRepository : BaseRepository<tblFilePaths>, IFilePathRepository
    {
        public FilePathRepository(MainContext Context) : base(Context)
        {

        }
    }
}
