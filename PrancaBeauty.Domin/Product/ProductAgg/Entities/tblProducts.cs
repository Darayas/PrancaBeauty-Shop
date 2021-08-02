using Framework.Domain;
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
        public DateTime Date { get; set; }
        public string Name { get; set; } // Uniqe Name
        public string Title { get; set; }
        public string Descreption { get; set; }
    }
}
