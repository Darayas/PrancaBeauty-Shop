using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductReviewsAttributeValuesAgg.Entities
{
    public class tblProductReviewsAttributeValues : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductReviewId { get; set; }
        public Guid ProductReviewAttributeId { get; set; }
        public double Value { get; set; }

        public virtual tblProductReviews tblProductReviews { get; set; }
        public virtual tblProductReviewsAttribute tblProductReviewsAttribute { get; set; }
    }
}
