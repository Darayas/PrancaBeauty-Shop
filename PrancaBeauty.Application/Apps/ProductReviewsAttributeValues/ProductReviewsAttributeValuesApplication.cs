using PrancaBeauty.Domin.Product.ProductReviewsAttributeValuesAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsAttributeValues
{
    public class ProductReviewsAttributeValuesApplication : IProductReviewsAttributeValuesApplication
    {
        private readonly IProductReviewsAttributeValuesRepository _ProductReviewsAttributeValuesRepository;

        public ProductReviewsAttributeValuesApplication(IProductReviewsAttributeValuesRepository productReviewsAttributeValuesRepository)
        {
            _ProductReviewsAttributeValuesRepository = productReviewsAttributeValuesRepository;
        }
    }
}
