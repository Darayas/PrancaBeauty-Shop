using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviews
{
    public class ProductReviewsRepository : BaseRepository<tblProductReviews>, IProductReviewsRepository
    {
        public ProductReviewsRepository(MainContext Context) : base(Context)
        {

        }
    }
}
