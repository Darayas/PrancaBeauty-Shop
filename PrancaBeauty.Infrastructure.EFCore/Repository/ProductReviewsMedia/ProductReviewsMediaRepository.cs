using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsMedia
{
    public class ProductReviewsMediaRepository : BaseRepository<tblProductReviewsMedia>, IProductReviewsMediaRepository
    {
        public ProductReviewsMediaRepository(MainContext Context) : base(Context)
        {

        }
    }
}
