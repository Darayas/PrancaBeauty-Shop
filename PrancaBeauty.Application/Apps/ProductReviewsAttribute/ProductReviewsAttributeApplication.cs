using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsAttribute
{
    public class ProductReviewsAttributeApplication : IProductReviewsAttributeApplication
    {
        private readonly IProductReviewsAttributeRepository _ProductReviewsAttributeRepository;

        public ProductReviewsAttributeApplication(IProductReviewsAttributeRepository productReviewsAttributeRepository)
        {
            _ProductReviewsAttributeRepository = productReviewsAttributeRepository;
        }
    }
}
