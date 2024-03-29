﻿using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Keywords.SearchHistoryAgg.Entities;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities;
using PrancaBeauty.Domin.Product.GuaranteeAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Entities;
using PrancaBeauty.Domin.Product.ProductPropertisAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Entities;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Entities;
using PrancaBeauty.Domin.Region.CityAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using PrancaBeauty.Domin.Region.ProvinceAgg.Entities;
using PrancaBeauty.Domin.Settings.SettingsAgg.Entities;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities;
using PrancaBeauty.Domin.Templates.TemplatesAgg.Entitis;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Region.LanguagesAgg.Entities
{
    public class tblLanguages : IEntity
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbr { get; set; }
        public string NativeName { get; set; }
        public bool IsRtl { get; set; }
        public bool IsActive { get; set; }
        public bool UseForSiteLanguage { get; set; }

        public virtual ICollection<tblTamplates> tblTamplates { get; set; }
        public virtual ICollection<tblSettings> tblSettings { get; set; }
        public virtual ICollection<tblCountries_Translates> tblCountries_Translates { get; set; }
        public virtual ICollection<tblProvinces_Translate> tblProvinces_Translate { get; set; }
        public virtual ICollection<tblCities_Translates> tblCities_Translates { get; set; }
        public virtual ICollection<tblUsers> tblUsers { get; set; }
        public virtual ICollection<tblCategory_Translates> tblCategory_Translates { get; set; }
        public virtual ICollection<tblProductTopic_Translates> tblProductTopic_Translates { get; set; }
        public virtual ICollection<tblProductPropertis_Translates> tblProductPropertis_Translates { get; set; }
        public virtual ICollection<tblProductVariants_Translates> tblProductVariants_Translates { get; set; }
        public virtual ICollection<tblSeller_Translates> tblSeller_Translates { get; set; }
        public virtual ICollection<tblProductReviewsAttribute_Translate> tblProductReviewsAttribute_Translate { get; set; }
        public virtual ICollection<tblProducts> tblProducts { get; set; }
        public virtual ICollection<tblCurrency_Translates> tblCurrency_Translates { get; set; }
        public virtual ICollection<tblGuarantee_Translates> tblGuarantee_Translates { get; set; }
        public virtual ICollection<tblShowcasesTranslates> tblShowCasesTranslate { get; set; }
        public virtual ICollection<tblShowcaseTabTranslates> tblShowcaseTabTranslates { get; set; }
        public virtual ICollection<tblSectionFreeItemTranslate> tblSectionFreeItemTranslate { get; set; }
        public virtual ICollection<tblSearchHistory> tblSearchHistory { get; set; }
        public virtual ICollection<tblPaymentGateTranslate> tblPaymentGateTranslate { get; set; }
        public virtual ICollection<tblShippingMethodTranslate> tblShippingMethodTranslate { get; set; }
        public virtual ICollection<tblProductGroupTranslate> tblProductGroupTranslate { get; set; }


        public virtual tblCountries tblCountries { get; set; }
    }
}
