using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsAttribute_Translate
{
    public class ProductReviewsAttribute_TranslateRepository : BaseRepository<tblProductReviewsAttribute>, IProductReviewsAttribute_TranslateRepository
    {
        public ProductReviewsAttribute_TranslateRepository(MainContext Context) : base(Context)
        {

        }
    }
}
