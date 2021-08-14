using Framework.Domain;
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
    }
}
