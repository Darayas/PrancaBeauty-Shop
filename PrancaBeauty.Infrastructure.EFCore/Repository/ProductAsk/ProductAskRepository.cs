using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductAskAgg.Contarcts;
using PrancaBeauty.Domin.Product.ProductAskAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductAsk
{
    public class ProductAskRepository : BaseRepository<tblProductAsk>, IProductAskRepository
    {
        public ProductAskRepository(MainContext Context) : base(Context)
        {

        }
    }
}
