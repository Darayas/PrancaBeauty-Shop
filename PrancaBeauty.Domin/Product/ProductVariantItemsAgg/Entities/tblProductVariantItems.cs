using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities
{
    public class tblProductVariantItems : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductVariantId { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public double Percent { get; set; }

        public virtual tblProductVariants tblProductVariants { get; set; }
        public virtual tblProducts tblProducts { get; set; }
    }
}
