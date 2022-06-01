using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Cart.CartAgg.Entities
{
    public class tblCarts : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid SellerId { get; set; }
        public Guid VariantItemId { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }

        public virtual tblProducts tblProducts { get; set; }
        public virtual tblUsers tblUsers { get; set; }
        public virtual tblProductVariantItems tblProductVariantItems { get; set; }
        public virtual tblSellers tblSellers { get; set; }
    }
}
