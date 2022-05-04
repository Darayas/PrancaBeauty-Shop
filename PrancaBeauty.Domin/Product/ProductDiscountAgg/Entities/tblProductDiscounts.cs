using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Product.ProductDiscountAgg.Entities
{
    public class tblProductDiscounts : IEntity
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UseableNum { get; set; } // تعداد قابل استفاده
        public int NumOfUses { get; set; } // تعداد استفاده شده
        public bool IsActive { get; set; } = true;
        public bool IsEnable { get; set; }
        public double Percent { get; set; } // درصد تخفیف

        public virtual tblSellers tblSellers { get; set; }
        public virtual ICollection<tblProductVariantItems> tblProductVariantItems { get; set; }

    }
}
