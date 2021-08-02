using PrancaBeauty.Domin.Product.ProductAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Products
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _ProductRepository;
        public ProductApplication(IProductRepository productRepository)
        {
            _ProductRepository = productRepository;
        }
    }
}
