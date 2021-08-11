using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsLike
{
    public class ProductReviewsLikeApplication : IProductReviewsLikeApplication
    {
        private readonly IProductReviewsLikeRepository _ProductReviewsLikeRepository;

        public ProductReviewsLikeApplication(IProductReviewsLikeRepository productReviewsLikeRepository)
        {
            _ProductReviewsLikeRepository = productReviewsLikeRepository;
        }
    }
}
