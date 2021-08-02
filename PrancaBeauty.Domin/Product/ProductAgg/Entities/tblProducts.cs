using Framework.Domain;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductAgg.Entities
{
    public class tblProducts : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; } // Uniqe Name
        public string Title { get; set; }
        public string Descreption { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual tblCategoris tblCategoris { get; set; }
        public virtual ICollection<tblKeywords_Products> tblKeywords_Products { get; set; }
    }
}
