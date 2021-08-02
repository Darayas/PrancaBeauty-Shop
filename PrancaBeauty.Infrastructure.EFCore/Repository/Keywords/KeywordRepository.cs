using Framework.Infrastructure;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Keywords
{
    public class KeywordRepository : BaseRepository<tblProducts>, IKeywordRepository
    {
        public KeywordRepository(MainContext Context) : base(Context)
        {

        }
    }
}
