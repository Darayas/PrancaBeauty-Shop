using PrancaBeauty.Domin.Product.ProductSellerAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductSellers
{
    public class ProductSellersApplication : IProductSellersApplication
    {
        private readonly IProductSellersRepsoitory _ProductSellersRepsoitory;

        public ProductSellersApplication(IProductSellersRepsoitory productSellersRepsoitory)
        {
            _ProductSellersRepsoitory = productSellersRepsoitory;
        }
    }
}
