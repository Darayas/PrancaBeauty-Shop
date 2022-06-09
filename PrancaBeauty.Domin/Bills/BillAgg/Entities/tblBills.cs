using Framework.Domain.Contracts;
using Framework.Domain.Enums;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Entities;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities;
using PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Bills.BillAgg.Entities
{
    public class tblBills : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? GateId { get; set; }
        public string BillNumber { get; set; }
        public double? TaxAmount { get; set; } // بعد از پرداخت کاربر تکمیل شود
        public double? TotalPrice { get; set; } // بعد از پرداخت کاربر تکمیل شود
        public string TransactionNumber { get; set; }
        public string GateNumber { get; set; }
        public DateTime Date { get; set; } // تاریخ تغییر وضعیت فاکتور
        public BillStatusEnum Status { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual tblPaymentGates tblPaymentGates { get; set; }
        public virtual ICollection<tblBillItems> tblBillItems { get; set; }
        public virtual ICollection<tblPostalBarcodes> tblPostalBarcodes { get; set; }

    }
}
