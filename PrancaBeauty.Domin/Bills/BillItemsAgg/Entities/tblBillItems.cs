using Framework.Domain.Contracts;
using System;

namespace PrancaBeauty.Domin.Bills.BillItemsAgg.Entities
{
    public class tblBillItems : IEntity
    {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public Guid ProductId { get; set; }
        public Guid VarianrItemId { get; set; }
        public Guid SellerId { get; set; }
        public int Qty { get; set; }
        public double? Price { get; set; } // زمان پرداخت مقداردهی میگردد
        public double? Tax { get; set; }// زمان پرداخت مقداردهی میگردد
        public double? TotalPrice { get; set; }// زمان پرداخت مقداردهی میگردد
    }
}
