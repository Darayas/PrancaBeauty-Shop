using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAskAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductAskLikesAgg.Entities
{
    public class tblProductAskLikes : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductAskId { get; set; }
        public Guid UserId { get; set; }
        public ProductAskLikesEnum Type { get; set; }
        public DateTime Date { get; set; }

        public virtual tblProductAsk tblProductAsk { get; set; }
        public virtual tblUsers tblUsers { get; set; }
    }

    public enum ProductAskLikesEnum
    {
        Like,
        Dislike
    }
}
