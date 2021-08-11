using PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductReviewsMedia
{
    public class ProductReviewsMediaApplication : IProductReviewsMediaApplication
    {
        private readonly IProductReviewsMediaRepository _ProductReviewsMediaRepository;

        public ProductReviewsMediaApplication(IProductReviewsMediaRepository productReviewsMediaRepository)
        {
            _ProductReviewsMediaRepository = productReviewsMediaRepository;
        }
    }
}
