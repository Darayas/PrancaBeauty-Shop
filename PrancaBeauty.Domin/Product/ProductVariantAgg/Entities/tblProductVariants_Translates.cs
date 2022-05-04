using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductVariantAgg.Entities
{
    public class tblProductVariants_Translates : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductVariantId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblProductVariants tblProductVariants { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }

    }
}
