using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.GuaranteeAgg.Entities
{
    public class tblGuarantee : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsEnable { get; set; }

        public virtual ICollection<tblGuarantee_Translates> tblGuarantee_Translates { get; set; }
        public virtual ICollection<tblProductVariantItems> tblProductVariantItems { get; set; }
    }
}
