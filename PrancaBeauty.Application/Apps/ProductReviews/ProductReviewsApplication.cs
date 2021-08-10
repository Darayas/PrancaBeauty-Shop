using PrancaBeauty.Domin.Product.ProductReviewsAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviews
{
    public class ProductReviewsApplication : IProductReviewsApplication
    {
        private readonly IProductReviewsRepository _ProductReviewsRepository;
        public ProductReviewsApplication(IProductReviewsRepository productReviewsRepository)
        {
            _ProductReviewsRepository = productReviewsRepository;
        }
    }
}
