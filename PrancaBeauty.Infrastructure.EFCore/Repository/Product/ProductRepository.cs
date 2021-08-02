using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Product
{
    public class ProductRepository : BaseRepository<tblProducts>, IProductRepository
    {
        public ProductRepository(MainContext Context) : base(Context)
        {

        }
    }
}
