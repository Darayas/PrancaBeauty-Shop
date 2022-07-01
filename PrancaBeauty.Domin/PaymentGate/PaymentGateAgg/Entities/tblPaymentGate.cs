using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Bills.BillAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.PaymentGate.PaymentGateRestrictAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities
{
    public class tblPaymentGates : IEntity
    {
        public Guid Id { get; set; }
        public Guid LogoId { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public bool IsEnable { get; set; }

        public virtual tblFiles tblFiles { get; set; }
        public virtual ICollection<tblPaymentGateTranslate> tblPaymentGateTranslate { get; set; }
        public virtual ICollection<tblPaymentGateRestricts> tblPaymentGateRestricts { get; set; }
        public virtual ICollection<tblBills> tblBills { get; set; }
    }
}
