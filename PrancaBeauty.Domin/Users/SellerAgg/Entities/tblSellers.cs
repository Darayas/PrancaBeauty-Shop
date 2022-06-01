using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Cart.CartAgg.Entities;
using PrancaBeauty.Domin.Product.ProductDiscountAgg.Entities;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Users.SellerAgg.Entities
{
    public class tblSellers : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirm { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual ICollection<tblSeller_Translates> tblSeller_Translates { get; set; }
        public virtual ICollection<tblProductSellers> tblProductSellers { get; set; }
        public virtual ICollection<tblProductDiscounts> tblProductDiscounts { get; set; }
        public virtual ICollection<tblCarts> tblCarts { get; set; }

    }
}
