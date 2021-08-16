using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsAttribute
{
    public class ProductReviewsAttributeRepository : BaseRepository<tblProductReviewsAttribute>, IProductReviewsAttributeRepository
    {
        public ProductReviewsAttributeRepository(MainContext Context) : base(Context)
        {

        }
    }
}
