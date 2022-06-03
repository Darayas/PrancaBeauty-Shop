using Framework.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Bills.BillAgg.Entities
{
    public class tblBills : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? GateId { get; set; }
        public Guid ShippingMethodId { get; set; }
        public string BillNumber { get; set; }
        public double? ShippingPrice { get; set; }
        public double? TaxAmount { get; set; }
        public double? TotalPrice { get; set; }
        public string TransactionNumber { get; set; }
        public string GateNumber { get; set; }
        public DateTime Date { get; set; } // تاریخ تغییر وضعیت فاکتور
        public bool IsPayyed { get; set; } // وضعیت پرداخت

    }
}
