using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities
{
    public class tblProductReviewsLikes : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductReviewId { get; set; }
        public Guid UserId { get; set; }
        public ProductReviewsLikesEnum Type { get; set; }
        public DateTime Date { get; set; }

        public virtual tblProductReviews tblProductReviews { get; set; }
        public virtual tblUsers tblUsers { get; set; }
    }

    public enum ProductReviewsLikesEnum
    {
        Like,
        Dislike
    }
}
