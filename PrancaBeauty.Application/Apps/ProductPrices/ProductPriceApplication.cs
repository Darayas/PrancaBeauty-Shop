using PrancaBeauty.Domin.Product.ProductPricesAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductPrices
{
    public class ProductPriceApplication : IProductPriceApplication
    {
        private readonly IProductPricesRepository _ProductPricesRepository;

        public ProductPriceApplication(IProductPricesRepository productPricesRepository)
        {
            _ProductPricesRepository = productPricesRepository;
        }
    }
}
