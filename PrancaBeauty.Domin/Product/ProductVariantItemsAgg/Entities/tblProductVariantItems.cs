using Framework.Domain;
using PrancaBeauty.Domin.Product.GuaranteeAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
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
        public Guid ProductSellerId { get; set; }
        public Guid? GuaranteeId { get; set; }
        public string ProductCode { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public double Percent { get; set; }
        public tblProductVariantItems_SendBy SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده
        public bool IsEnable { get; set; }
        public tblProductVariantItems_SendFrom SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده
        public int CountInStock { get; set; }

        public virtual tblProductVariants tblProductVariants { get; set; }
        public virtual tblProducts tblProducts { get; set; }
        public virtual tblProductSellers tblProductSellers { get; set; }
        public virtual tblGuarantee tblGuarantee { get; set; }
    }

    public enum tblProductVariantItems_SendBy
    {
        Prancabeauty = 0,
        Seller = 1
    }

    public enum tblProductVariantItems_SendFrom
    {
        OneDay = 1,
        TwoDays = 2,
        ThreeDays = 3,
        FourDays = 4
    }
}
