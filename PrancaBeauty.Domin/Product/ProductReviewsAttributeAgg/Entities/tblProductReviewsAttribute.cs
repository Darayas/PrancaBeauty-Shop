using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeValuesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Entities
{
    public class tblProductReviewsAttribute : IEntity
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public string Name { get; set; }

        public virtual tblProductTopic tblProductTopic { get; set; }
        public virtual ICollection<tblProductReviewsAttribute_Translate> tblProductReviewsAttribute_Translate { get; set; }
        public virtual ICollection<tblProductReviewsAttributeValues> tblProductReviewsAttributeValues { get; set; }

    }
}
