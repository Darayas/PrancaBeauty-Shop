using Framework.Application.Enums;
using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Entities;
using PrancaBeauty.Domin.Cart.CartAgg.Entities;
using PrancaBeauty.Domin.Product.GuaranteeAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductDiscountAgg.Entities;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletDepositDetailsAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities
{
    public class tblProductVariantItems : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductVariantId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSellerId { get; set; }
        public Guid? GuaranteeId { get; set; }
        public Guid? DiscountId { get; set; }
        public string ProductCode { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public double Percent { get; set; }
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده
        public int CountInStock { get; set; }

        public bool IsEnable { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsMain { get; set; }

        public virtual tblProductVariants tblProductVariants { get; set; }
        public virtual tblProducts tblProducts { get; set; }
        public virtual tblProductSellers tblProductSellers { get; set; }
        public virtual tblGuarantee tblGuarantee { get; set; }
        public virtual tblProductDiscounts tblProductDiscounts { get; set; }
        public virtual ICollection<tblCarts> tblCarts { get; set; }
        public virtual ICollection<tblBillItems> tblBillItems { get; set; }
        public virtual ICollection<tblWalletDepositDetails> tblWalletDepositDetails { get; set; }

    }
}
