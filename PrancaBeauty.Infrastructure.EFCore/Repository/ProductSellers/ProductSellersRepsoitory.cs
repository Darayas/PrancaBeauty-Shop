using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductSellers
{
    public class ProductSellersRepsoitory : BaseRepository<tblProductSellers>, IProductSellersRepsoitory
    {
        public ProductSellersRepsoitory(MainContext Context) : base(Context)
        {

        }
    }
}
