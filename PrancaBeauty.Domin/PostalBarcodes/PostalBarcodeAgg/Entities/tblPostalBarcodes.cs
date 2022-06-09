using Framework.Domain.Contracts;
using Framework.Domain.Enums;
using PrancaBeauty.Domin.Bills.BillAgg.Entities;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Entities;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Entities
{
    public class tblPostalBarcodes : IEntity
    {
        public Guid Id { get; set; }
        public Guid BillId { get; set; }
        public Guid ShippingMethodId { get; set; }
        public DateTime Date { get; set; } // تاریخ ثبت رکورد جاری
        public int Weight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public double TotalPrice { get; set; }
        public string Barcode { get; set; }
        public PostalBarcodeEnum Status { get; set; }
        public DateTime ChangeStatusDateTime { get; set; }


        public virtual tblBills tblBill { get; set; }
        public virtual tblShippingMethods tblShippingMethod { get; set; }
        public virtual ICollection<tblBillItems> tblBillItems { get; set; }
    }
}
