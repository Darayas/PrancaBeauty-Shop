using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductAskAgg.Entities
{
    public class tblProductAsk : IEntity
    {
        public Guid Id { get; set; }
        public Guid? AskId { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsConfirm { get; set; }


        public virtual tblProducts tblProducts { get; set; }
        public virtual tblUsers tblUsers { get; set; }
        public virtual tblProductAsk tblProductAsk_Parent { get; set; }
        public virtual ICollection<tblProductAsk> tblProductAsk_Childs { get; set; }
        public virtual ICollection<tblProductAskLikes> tblProductAskLikes { get; set; }
    }
}
