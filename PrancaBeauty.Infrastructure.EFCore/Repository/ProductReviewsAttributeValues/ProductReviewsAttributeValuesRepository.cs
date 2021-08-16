using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeValuesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeValuesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsAttributeValues
{
    public class ProductReviewsAttributeValuesRepository : BaseRepository<tblProductReviewsAttributeValues>, IProductReviewsAttributeValuesRepository
    {
        public ProductReviewsAttributeValuesRepository(MainContext Context) : base(Context)
        {

        }
    }
}
