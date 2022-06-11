using Framework.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmBill
    {
        public string GateId { get; set; }
        public string AddressId { get; set; }
        public double TotalPrice { get; set; }
        public double TaxAmount { get; set; }
        public double ShippingAmount { get; set; }
        public string Note { get; set; }
        public string TransactionNumber { get; set; }
        public BillStatusEnum BillStatus { get; set; }

        public List<vmBillItemGroups> LstSellerGroups { get; set; }
    }

    public class vmBillItemGroups
    {
        public string SellerName { get; set; }
        public string ShippingMethodId { get; set; }
        public double TotalPrice { get; set; }
        public double TaxAmount { get; set; }
        public double ShippingAmount { get; set; }
        public PostalBarcodeEnum ShippingStatus { get; set; }
        public string Barcode { get; set; }
        public DateTime ChangeStatusDateTime { get; set; }

        public List<vmBillItems> LstItems { get; set; }
    }

    public class vmBillItems
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string VariantTopic { get; set; }
        public string VariantValue { get; set; }
        public int Qty { get; set; }
        public string CurrencySymbol { get; set; }
        public double TotalAmount { get; set; }
    }
}
