using Framework.Infrastructure;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Contracts;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Keywords_Products
{
    public class Keywords_ProductsRepository : BaseRepository<tblKeywords_Products>, IKeywords_ProductsRepository
    {
        public Keywords_ProductsRepository(MainContext Context) : base(Context)
        {

        }
    }
}
