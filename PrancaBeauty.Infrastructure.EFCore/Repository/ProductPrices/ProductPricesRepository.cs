using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductPrices
{
    public class ProductPricesRepository : BaseRepository<tblProductPrices>, IProductPricesRepository
    {
        public ProductPricesRepository(MainContext context) : base(context)
        {

        }
    }
}
