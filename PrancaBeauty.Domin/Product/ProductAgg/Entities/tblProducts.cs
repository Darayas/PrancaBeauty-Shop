﻿using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Entities;
using PrancaBeauty.Domin.Bills.TaxAgg.Entities;
using PrancaBeauty.Domin.Cart.CartAgg.Entities;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Entities;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Entites;
using PrancaBeauty.Domin.Product.ProductAskAgg.Entities;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Entities;
using PrancaBeauty.Domin.Product.ProductMediaAgg.Entities;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionProductAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletProductDepositDetailsAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Product.ProductAgg.Entities
{
    public class tblProducts : IEntity
    {

        public Guid Id { get; set; }
        public Guid? TopicId { get; set; }
        public Guid AuthorUserId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? TaxGroupId { get; set; }
        public Guid? ProductGroupId { get; set; }
        public Guid LangId { get; set; }
        public DateTime Date { get; set; }
        public string UniqueNumber { get; set; } // Uniqe Number
        public string Name { get; set; } // Uniqe Name
        public string Title { get; set; }
        public string Description { get; set; }
        public bool ItsForConfirm { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDraft { get; set; }
        public bool IsDelete { get; set; }

        public string MetaTagKeyword { get; set; }
        public string MetaTagCanonical { get; set; }
        public string MetaTagDescreption { get; set; }

        public bool Incomplete { get; set; }
        public string IncompleteReason { get; set; }

        public virtual tblUsers tblAuthorUser { get; set; }
        public virtual tblProductTopic tblProductTopic { get; set; }
        public virtual tblCategoris tblCategory { get; set; }
        public virtual tblLanguages tblLanguage { get; set; }
        public virtual tblTaxGroups tblTaxGroups { get; set; }
        public virtual tblProductGroups tblProductGroups { get; set; }
        public virtual ICollection<tblKeywords_Products> tblKeywords_Products { get; set; }
        public virtual ICollection<tblProductPrices> tblProductPrices { get; set; }
        public virtual ICollection<tblProductMedia> tblProductMedia { get; set; }
        public virtual ICollection<tblProductReviews> tblProductReviews { get; set; }
        public virtual ICollection<tblProductAsk> tblProductAsk { get; set; }
        public virtual ICollection<tblProductPropertiesValues> tblProductPropertiesValues { get; set; }
        public virtual ICollection<tblProductVariantItems> tblProductVariantItems { get; set; }
        public virtual ICollection<tblProductSellers> tblProductSellers { get; set; }
        public virtual ICollection<tblPostingRestrictions> tblPostingRestrictions { get; set; }
        public virtual ICollection<tblSectionProducts> tblSectionProducts { get; set; }
        public virtual ICollection<tblCarts> tblCarts { get; set; }
        public virtual ICollection<tblBillItems> tblBillItems { get; set; }
        public virtual ICollection<tblWalletProductDepositDetails> tblWalletProductDepositDetails { get; set; }
    }
}
