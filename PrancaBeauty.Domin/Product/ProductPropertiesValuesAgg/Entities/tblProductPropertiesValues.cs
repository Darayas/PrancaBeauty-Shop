using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductPropertisAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Entities
{
    public class tblProductPropertiesValues : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductPropertiesId { get; set; }
        public Guid ProductId { get; set; }
        public string Value { get; set; }

        public virtual tblProductPropertis tblProductPropertis { get; set; }
        public virtual tblProducts tblProducts { get; set; }
    }
}
