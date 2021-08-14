using PrancaBeauty.Domin.Product.ProductVariantAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductVariants
{
    public class ProductVariantsApplication : IProductVariantsApplication
    {
        private readonly IProductVariantsRepository _ProductVariantsRepository;

        public ProductVariantsApplication(IProductVariantsRepository productVariantsRepository)
        {
            _ProductVariantsRepository = productVariantsRepository;
        }
    }
}
