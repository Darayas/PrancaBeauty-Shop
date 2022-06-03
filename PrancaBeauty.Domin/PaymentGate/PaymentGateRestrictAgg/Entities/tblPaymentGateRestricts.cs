using Framework.Domain.Contracts;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using System;

namespace PrancaBeauty.Domin.PaymentGate.PaymentGateRestrictAgg.Entities
{
    public class tblPaymentGateRestricts : IEntity
    {
        public Guid Id { get; set; }
        public Guid PaymentGateId { get; set; }
        public Guid CountryId { get; set; }
        public Guid CurrencyId { get; set; }
        public bool IsEnable { get; set; }

        public virtual tblPaymentGates tblPaymentGates { get; set; }
        public virtual tblCountries tblCountry { get; set; }
        public virtual tblCurrencies tblCurrency { get; set; }
    }
}
