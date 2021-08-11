using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Entities
{
    public class tblProductReviewsMedia : IEntity
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public Guid ProductReviewsId { get; set; }
        public int Sort { get; set; }


        public virtual tblProductReviews tblProductReviews { get; set; }
        public virtual tblFiles tblFiles { get; set; }
    }
}
