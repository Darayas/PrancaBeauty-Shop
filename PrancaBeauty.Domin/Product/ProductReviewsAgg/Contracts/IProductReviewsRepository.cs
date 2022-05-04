using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductReviewsAgg.Contracts
{
    public interface IProductReviewsRepository : IRepository<tblProductReviews>
    {
    }
}
