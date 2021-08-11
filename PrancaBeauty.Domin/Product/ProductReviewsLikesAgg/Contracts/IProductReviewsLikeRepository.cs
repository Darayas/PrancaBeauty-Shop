using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Contracts
{
    public interface IProductReviewsLikeRepository : IRepository<tblProductReviewsLikes>
    {
    }
}
