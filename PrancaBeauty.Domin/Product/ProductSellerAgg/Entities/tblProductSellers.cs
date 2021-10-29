﻿using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductSellerAgg.Entities
{
    public class tblProductSellers : IEntity
    {
        public Guid Id { get; set; }
        public Guid SellerUserId { get; set; }
        public Guid ProductId { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirm { get; set; }

        public virtual tblProducts tblProducts { get; set; }
        public virtual tblUsers tblUsers { get; set; }
        public virtual ICollection<tblProductReviews> tblProductReviews { get; set; }
        public virtual ICollection<tblProductVariantItems> tblProductVariantItems { get; set; }
    }
}
