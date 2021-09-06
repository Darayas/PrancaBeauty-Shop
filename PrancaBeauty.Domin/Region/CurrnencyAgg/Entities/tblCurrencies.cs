using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.CurrnencyAgg.Entities
{
    public class tblCurrencies : IEntity
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public virtual tblCountries tblCountry { get; set; }
        public virtual ICollection<tblCurrency_Translates> tblCurrency_Translates { get; set; }
        public virtual ICollection<tblProductPrices> tblProductPrices { get; set; }
    }
}
