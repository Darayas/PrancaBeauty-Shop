using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Entities;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities
{
    public class tblProductReviews : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSellerId { get; set; } // کاربر فروشنده
        public Guid AuthorUserId { get; set; } // کاربر نویسنده دیدگاه
        public bool IsRead { get; set; }
        public bool IsConfirm { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string IpAddress { get; set; }
        public int CountStar { get; set; }

        public virtual tblProducts tblProducts { get; set; }
        public virtual tblProductSellers tblProductSellers { get; set; }
        public virtual tblUsers tblUsers { get; set; }
        public virtual ICollection<tblProductReviewsLikes> tblProductReviewsLikes { get; set; }
        public virtual ICollection<tblProductReviewsMedia> tblProductReviewsMedia { get; set; }
    }
}
