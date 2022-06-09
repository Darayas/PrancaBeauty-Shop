using Framework.Domain.Contracts;
using Framework.Domain.Enums;
using PrancaBeauty.Domin.Bills.BillAgg.Entities;
using PrancaBeauty.Domin.Bills.TaxAgg.Entities;
using PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
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
        public Guid TaxGroupId { get; set; }
        public Guid PostalBarcodeId { get; set; }
        public int Qty { get; set; }
        public double? Price { get; set; } // زمان پرداخت مقداردهی میگردد
        public double? TaxPercent { get; set; } // زمان پرداخت مقداردهی میگردد
        public double? TotalPrice { get; set; } // زمان پرداخت مقداردهی میگردد
        public BillItemStatusEnum ReturnStatus { get; set; }
        public string ReasonReturn { get; set; }

        public virtual tblBills tblBills { get; set; }
        public virtual tblProducts tblProducts { get; set; }
        public virtual tblProductVariantItems tblProductVariantItems { get; set; }
        public virtual tblSellers tblSellers { get; set; }
        public virtual tblPostalBarcodes tblPostalBarcodes { get; set; }
        public virtual tblTaxGroups tblTaxGroups { get; set; }

    }
}
