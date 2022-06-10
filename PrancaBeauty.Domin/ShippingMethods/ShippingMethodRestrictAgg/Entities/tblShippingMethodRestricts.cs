using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.CityAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Region.ProvinceAgg.Entities;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities;
using System;

namespace PrancaBeauty.Domin.ShippingMethods.ShippingMethodRestrictAgg.Entities
{
    public class tblShippingMethodRestricts : IEntity
    {
        public Guid Id { get; set; }
        public Guid ShippingMethodId { get; set; }
        public Guid CountryId { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid CityId { get; set; }
        public bool IsActive { get; set; }

        public virtual tblShippingMethods tblShippingMethods { get; set; }
        public virtual tblCountries tblCountry { get; set; }
        public virtual tblProvinces tblProvinces { get; set; }
        public virtual tblCities tblCity { get; set; }
    }
}
