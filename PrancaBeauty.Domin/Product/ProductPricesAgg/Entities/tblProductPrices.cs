using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductPricesAgg.Entities
{
    public class tblProductPrices : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }

        public virtual tblProducts tblProducts { get; set; }
        public virtual tblUsers tblUsers { get; set; }
    }
}
