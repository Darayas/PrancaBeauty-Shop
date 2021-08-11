using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsLike
{
    public class ProductReviewsLikeRepository : BaseRepository<tblProductReviewsLikes>, IProductReviewsLikeRepository
    {
        public ProductReviewsLikeRepository(MainContext Context) : base(Context)
        {

        }
    }
}
