using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductPropertisAgg.Entities
{
    public class tblProductPropertis_Translates : IEntity
    {
        public Guid Id { get; set; }
        public Guid PropertyId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblProductPropertis tblProductPropertis { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }

    }
}
