using Framework.Domain;
using PrancaBeauty.Domin.Region.CityAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.ProvinceAgg.Entities
{
    public class tblProvinces : IEntity
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual tblCountries tblCountry { get; set; }
        public virtual ICollection<tblProvinces_Translate> tblProvinces_Translate { get; set; }
        public virtual ICollection<tblCities> tblCities { get; set; }
        public virtual ICollection<tblAddress> tblAddress { get; set; }
    }
}
