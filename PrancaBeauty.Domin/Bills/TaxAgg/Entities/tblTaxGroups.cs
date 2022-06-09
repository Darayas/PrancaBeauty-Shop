using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Bills.TaxAgg.Entities
{
    public class tblTaxGroups : IEntity
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Percent { get; set; }

        public virtual tblCountries tblCountry { get; set; }
        public virtual ICollection<tblProducts> tblProducts { get; set; }
        public virtual ICollection<tblBillItems> tblBillItems { get; set; }
    }
}
