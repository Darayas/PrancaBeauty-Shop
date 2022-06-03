using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;

namespace PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities
{
    public class tblPaymentGateTranslate : IEntity
    {
        public Guid Id { get; set; }
        public Guid PaymentGateId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblPaymentGates tblPaymentGates { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
    }
}
