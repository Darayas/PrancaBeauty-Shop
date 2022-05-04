using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductVariantAgg.Entities
{
    public class tblProductVariants : IEntity
    {
        public Guid Id { get; set; }
        public string VariantType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<tblProductVariants_Translates> tblProductVariants_Translates { get; set; }
        public virtual ICollection<tblProductVariantItems> tblProductVariantItems { get; set; }
    }
}
