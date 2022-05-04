using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Entities
{
    public class tblProductReviewsAttribute_Translate : IEntity
    {
        public Guid Id { get; set; }
        public Guid LangId { get; set; }
        public Guid ProductReviewsAttributeId { get; set; }
        public string Title { get; set; }

        public virtual tblProductReviewsAttribute tblProductReviewsAttribute { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
    }
}
