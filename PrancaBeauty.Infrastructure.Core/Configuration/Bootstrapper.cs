﻿using Framework.Application.Services.Email;
using Framework.Application.Services.FTP;
using Framework.Application.Services.IpList;
using Framework.Application.Services.Security.AntiShell;
using Framework.Application.Services.Sms;
using Framework.Common.Utilities.Downloader;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PrancaBeauty.Application.Apps.Accesslevels;
using PrancaBeauty.Application.Apps.AccesslevelsRoles;
using PrancaBeauty.Application.Apps.Address;
using PrancaBeauty.Application.Apps.BankAccount;
using PrancaBeauty.Application.Apps.BillItems;
using PrancaBeauty.Application.Apps.Bills;
using PrancaBeauty.Application.Apps.Carts;
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.Application.Apps.Cities;
using PrancaBeauty.Application.Apps.Countries;
using PrancaBeauty.Application.Apps.Currency;
using PrancaBeauty.Application.Apps.FilePath;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.Application.Apps.FileServer;
using PrancaBeauty.Application.Apps.FileTypes;
using PrancaBeauty.Application.Apps.Guarantee;
using PrancaBeauty.Application.Apps.Keywords;
using PrancaBeauty.Application.Apps.KeywordsProducts;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.PaymentGateRestricts;
using PrancaBeauty.Application.Apps.PaymentGates;
using PrancaBeauty.Application.Apps.PostalBarcode;
using PrancaBeauty.Application.Apps.PostingRestrictions;
using PrancaBeauty.Application.Apps.ProductAsk;
using PrancaBeauty.Application.Apps.ProductAskLikes;
using PrancaBeauty.Application.Apps.ProductDiscount;
using PrancaBeauty.Application.Apps.ProductGroup;
using PrancaBeauty.Application.Apps.ProductGroupPercent;
using PrancaBeauty.Application.Apps.ProductMedia;
using PrancaBeauty.Application.Apps.ProductPrices;
using PrancaBeauty.Application.Apps.ProductPropertiesValues;
using PrancaBeauty.Application.Apps.ProductPropertis;
using PrancaBeauty.Application.Apps.ProductReviews;
using PrancaBeauty.Application.Apps.ProductReviewsAttribute;
using PrancaBeauty.Application.Apps.ProductReviewsAttributeValues;
using PrancaBeauty.Application.Apps.ProductReviewsLike;
using PrancaBeauty.Application.Apps.ProductReviewsMedia;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.Application.Apps.ProductTopic;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Apps.ProductVariants;
using PrancaBeauty.Application.Apps.Province;
using PrancaBeauty.Application.Apps.Roles;
using PrancaBeauty.Application.Apps.SearchHistory;
using PrancaBeauty.Application.Apps.SectionFreeItem;
using PrancaBeauty.Application.Apps.SectionItems;
using PrancaBeauty.Application.Apps.SectionProduct;
using PrancaBeauty.Application.Apps.SectionProductCategory;
using PrancaBeauty.Application.Apps.SectionProductKeyword;
using PrancaBeauty.Application.Apps.Seller;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Apps.ShippingMethodRestricts;
using PrancaBeauty.Application.Apps.ShippingMethods;
using PrancaBeauty.Application.Apps.Showcases;
using PrancaBeauty.Application.Apps.ShowcaseTabs;
using PrancaBeauty.Application.Apps.ShowcaseTabSection;
using PrancaBeauty.Application.Apps.Slider;
using PrancaBeauty.Application.Apps.TaxGroups;
using PrancaBeauty.Application.Apps.Templates;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Apps.WalletDetails;
using PrancaBeauty.Application.Apps.WalletProductDepositDetails;
using PrancaBeauty.Application.Apps.Wallets;
using PrancaBeauty.Application.Apps.WalletWithdraw;
using PrancaBeauty.Application.Common.ExMethods;
using PrancaBeauty.Application.Common.FtpWapper;
using PrancaBeauty.Domin.BankAccount.BankAccountAgg.Contracts;
using PrancaBeauty.Domin.Bills.BillAgg.Contracts;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Contracts;
using PrancaBeauty.Domin.Bills.TaxAgg.Contracts;
using PrancaBeauty.Domin.Cart.CartAgg.Contracts;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FileAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FilePathAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FileTypeAgg.Contracts;
using PrancaBeauty.Domin.FileServer.ServerAgg.Contracts;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Contracts;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Contracts;
using PrancaBeauty.Domin.Keywords.SearchHistoryAgg.Contracts;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Contracts;
using PrancaBeauty.Domin.PaymentGate.PaymentGateRestrictAgg.Contracts;
using PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Contracts;
using PrancaBeauty.Domin.Product.GuaranteeAgg.Contracts;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductAskAgg.Contarcts;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductDiscountAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductGroupAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductGroupPercentAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductMediaAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPropertisAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAttributeValuesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantItemsAgg.Contracts;
using PrancaBeauty.Domin.Region.CityAgg.Contracts;
using PrancaBeauty.Domin.Region.CountryAgg.Contracts;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Contracts;
using PrancaBeauty.Domin.Region.ProvinceAgg.Contracts;
using PrancaBeauty.Domin.Settings.SettingsAgg.Contracts;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodAgg.Contracts;
using PrancaBeauty.Domin.ShippingMethods.ShippingMethodRestrictAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionItems.Contracts;
using PrancaBeauty.Domin.Showcases.SectionProductAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Contracts;
using PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Contracts;
using PrancaBeauty.Domin.Sliders.SliderAgg.Contracts;
using PrancaBeauty.Domin.Templates.TemplatesAgg.Contracts;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Contracts;
using PrancaBeauty.Domin.Users.AddressAgg.Contracts;
using PrancaBeauty.Domin.Users.RoleAgg.Contracts;
using PrancaBeauty.Domin.Users.SellerAgg.Contracts;
using PrancaBeauty.Domin.Users.UserAgg.Contracts;
using PrancaBeauty.Domin.Wallet.WalletAgg.Contracts;
using PrancaBeauty.Domin.Wallet.WalletDetailsAgg.Contracts;
using PrancaBeauty.Domin.Wallet.WalletProductDepositDetailsAgg.Contracts;
using PrancaBeauty.Domin.Wallet.WalletWithdrawAgg.Contracts;
using PrancaBeauty.Infrastructure.EFCore.Context;
using PrancaBeauty.Infrastructure.EFCore.Repository.AccessLevel;
using PrancaBeauty.Infrastructure.EFCore.Repository.Address;
using PrancaBeauty.Infrastructure.EFCore.Repository.BankAccounts;
using PrancaBeauty.Infrastructure.EFCore.Repository.BillItems;
using PrancaBeauty.Infrastructure.EFCore.Repository.Bills;
using PrancaBeauty.Infrastructure.EFCore.Repository.Cart;
using PrancaBeauty.Infrastructure.EFCore.Repository.Categories;
using PrancaBeauty.Infrastructure.EFCore.Repository.Cities;
using PrancaBeauty.Infrastructure.EFCore.Repository.Counties;
using PrancaBeauty.Infrastructure.EFCore.Repository.Currency;
using PrancaBeauty.Infrastructure.EFCore.Repository.FilePath;
using PrancaBeauty.Infrastructure.EFCore.Repository.FileServer;
using PrancaBeauty.Infrastructure.EFCore.Repository.FileType;
using PrancaBeauty.Infrastructure.EFCore.Repository.Guarantee;
using PrancaBeauty.Infrastructure.EFCore.Repository.Keywords;
using PrancaBeauty.Infrastructure.EFCore.Repository.Keywords_Products;
using PrancaBeauty.Infrastructure.EFCore.Repository.PaymentGateRestricts;
using PrancaBeauty.Infrastructure.EFCore.Repository.PaymentGates;
using PrancaBeauty.Infrastructure.EFCore.Repository.PostalBarcode;
using PrancaBeauty.Infrastructure.EFCore.Repository.PostingRestrictions;
using PrancaBeauty.Infrastructure.EFCore.Repository.Product;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductAsk;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductAskLikes;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductDiscount;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductGroupPercent;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductGroups;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductGroupTranslate;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductMedia;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductPrices;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductProperties;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductPropertiesValues;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviews;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsAttribute;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsAttribute_Translate;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsAttributeValues;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsLike;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsMedia;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductSellers;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductTopic;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductVariantItems;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductVariants;
using PrancaBeauty.Infrastructure.EFCore.Repository.Province;
using PrancaBeauty.Infrastructure.EFCore.Repository.Region;
using PrancaBeauty.Infrastructure.EFCore.Repository.Roles;
using PrancaBeauty.Infrastructure.EFCore.Repository.SearchHistory;
using PrancaBeauty.Infrastructure.EFCore.Repository.SectionFreeItem;
using PrancaBeauty.Infrastructure.EFCore.Repository.SectionItems;
using PrancaBeauty.Infrastructure.EFCore.Repository.SectionProduct;
using PrancaBeauty.Infrastructure.EFCore.Repository.SectionProductCategory;
using PrancaBeauty.Infrastructure.EFCore.Repository.SectionProductKeyword;
using PrancaBeauty.Infrastructure.EFCore.Repository.Sellers;
using PrancaBeauty.Infrastructure.EFCore.Repository.Settings;
using PrancaBeauty.Infrastructure.EFCore.Repository.ShippingMethod;
using PrancaBeauty.Infrastructure.EFCore.Repository.ShippingMethodRestricts;
using PrancaBeauty.Infrastructure.EFCore.Repository.Showcases;
using PrancaBeauty.Infrastructure.EFCore.Repository.ShowcaseTabs;
using PrancaBeauty.Infrastructure.EFCore.Repository.ShowcaseTabSection;
using PrancaBeauty.Infrastructure.EFCore.Repository.Slider;
using PrancaBeauty.Infrastructure.EFCore.Repository.TaxGroup;
using PrancaBeauty.Infrastructure.EFCore.Repository.Templates;
using PrancaBeauty.Infrastructure.EFCore.Repository.Users;
using PrancaBeauty.Infrastructure.EFCore.Repository.Wallet;
using PrancaBeauty.Infrastructure.EFCore.Repository.WalletDetails;
using PrancaBeauty.Infrastructure.EFCore.Repository.WalletProductDepositDetails;
using PrancaBeauty.Infrastructure.EFCore.Repository.WalletWithdraw;
using PrancaBeauty.Infrastructure.Logger.Serilogger;

namespace PrancaBeauty.Infrastructure.Core.Configuration
{
    public static class Bootstrapper
    {
        public static void Config(this IServiceCollection services)
        {
            services.AddCustomAutoMapper();
            services.AddDbContext<MainContext>(opt => opt.UseSqlServer("Server=.;Database=PrancaBeautyDb;Trusted_Connection=True;"));

            services.AddSingleton<ILogger, Serilogger>();
            services.AddSingleton<IEmailSender, GmailSender>();
            services.AddSingleton<ISmsSender, KaveNegarSmsSender>();
            services.AddSingleton<IDownloader, Downloader>();
            services.AddSingleton<IIPList, IPList>();
            services.AddSingleton<IAniShell, AniShell>();
            services.AddScoped<IFtpClient, FtpClient>();
            services.AddScoped<IFtpWapper, FtpWapper>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IAccesslevelRepository, AccessLevelRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IFileServerRepository, FileServerRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IAccesslevelRolesRepository, AccesslevelRolesRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategory_TranslateRepository, Category_TranslateRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IKeywordRepository, KeywordRepository>();
            services.AddScoped<IKeywords_ProductsRepository, Keywords_ProductsRepository>();
            services.AddScoped<IProductPricesRepository, ProductPricesRepository>();
            services.AddScoped<IProductMediaRepository, ProductMediaRepository>();
            services.AddScoped<IProductReviewsRepository, ProductReviewsRepository>();
            services.AddScoped<IProductReviewsLikeRepository, ProductReviewsLikeRepository>();
            services.AddScoped<IProductAskRepository, ProductAskRepository>();
            services.AddScoped<IProductAskLikesRepository, ProductAskLikesRepository>();
            services.AddScoped<IProductReviewsMediaRepository, ProductReviewsMediaRepository>();
            services.AddScoped<IProductTopicRepository, ProductTopicRepository>();
            services.AddScoped<IProductTopic_TranslatesRepository, ProductTopic_TranslatesRepository>();
            services.AddScoped<IProductPropertisRepository, ProductPropertisRepository>();
            services.AddScoped<IProductPropertisRepository, ProductPropertisRepository>();
            services.AddScoped<IProductPropertiesValuesRepository, ProductPropertiesValuesRepository>();
            services.AddScoped<IProductVariantsRepository, ProductVariantsRepository>();
            services.AddScoped<IProductVariants_TranslatesRepository, ProductVariants_TranslatesRepository>();
            services.AddScoped<IProductVariantItemsRepository, ProductVariantItemsRepository>();
            services.AddScoped<IProductSellersRepsoitory, ProductSellersRepsoitory>();
            services.AddScoped<ISellersRepository, SellersRepository>();
            services.AddScoped<ISeller_TranslatesRepository, Seller_TranslatesRepository>();
            services.AddScoped<IProductReviewsAttributeRepository, ProductReviewsAttributeRepository>();
            services.AddScoped<IProductReviewsAttribute_TranslateRepository, ProductReviewsAttribute_TranslateRepository>();
            services.AddScoped<IProductReviewsAttributeValuesRepository, ProductReviewsAttributeValuesRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICurrency_TranslatesRepository, Currency_TranslatesRepository>();
            services.AddScoped<IFileTypeRepository, FileTypeRepository>();
            services.AddScoped<IFilePathRepository, FilePathRepository>();
            services.AddScoped<IGuaranteeRepository, GuaranteeRepository>();
            services.AddScoped<IGuarantee_TranslatesRepository, Guarantee_TranslatesRepository>();
            services.AddScoped<IPostingRestrictionsRepository, PostingRestrictionsRepository>();
            services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IShowcaseRepository, ShowcaseRepository>();
            services.AddScoped<IShowcaseTranslateRepository, ShowcaseTranslateRepository>();
            services.AddScoped<IShowcaseTabsRepository, ShowcaseTabRepository>();
            services.AddScoped<IShowcaseTabsTranslateRepository, ShowcaseTabTranslateRepository>();
            services.AddScoped<IShowcaseTabSectionRepository, ShowcaseTabSectionRepository>();
            services.AddScoped<ISectionFreeItemRepository, SectionFreeItemRepository>();
            services.AddScoped<IShowcaseTabSectionFreeItemTranslateRepository, SectionFreeItemTranslateRepository>();
            services.AddScoped<ISectionProductRepository, SectionProductRepository>();
            services.AddScoped<ISectionProductCategoryRepository, SectionProductCategoryRepository>();
            services.AddScoped<ISectionProductKeywordRepository, SectionProductKeywordRepository>();
            services.AddScoped<IShowcaseTabSectionItemRepository, ShowcaseTabSectionItemRepository>();
            services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IPaymentGateRepository, PaymentGateRepository>();
            services.AddScoped<IPaymentGateTranslateRepository, PaymentGateTranslateRepository>();
            services.AddScoped<IPaymentGateRestrictRepository, PaymentGateRestrictRepository>();
            services.AddScoped<IShippingMethodRepository, ShippingMethodRepository>();
            services.AddScoped<IShippingMethodRepository, ShippingMethodTranslateRepository>();
            services.AddScoped<IShippingMethodRestrictRepository, ShippingMethodRestrictRepository>();
            services.AddScoped<IBillsRepository, BillsRepository>();
            services.AddScoped<IBillItemsRepository, BillItemsRepository>();
            services.AddScoped<IPostalBarcodeRepository, PostalBarcodeRepository>();
            services.AddScoped<ITaxGroupRepository, TaxGroupRepository>();
            services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            services.AddScoped<IProductGroupTranslateRepository, ProductGroupTranslateRepository>();
            services.AddScoped<IProductGroupPercentRepository, ProductGroupPercentRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IWalletDetailsRepository, WalletDetailsRepository>();
            services.AddScoped<IWalletProductDepositDetailsRepository, WalletProductDepositDetailsRepository>();
            services.AddScoped<IWalletWithdrawRepository, WalletWithdrawRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            // Applications
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<ISettingApplication, SettingApplication>();
            services.AddScoped<ITemplateApplication, TemplateApplication>();
            services.AddScoped<IAccesslevelApplication, AccesslevelApplication>();
            services.AddScoped<IRoleApplication, RoleApplication>();
            services.AddScoped<ILanguageApplication, LanguageApplication>();
            services.AddScoped<IFileServerApplication, FileServerApplication>();
            services.AddScoped<IFileApplication, FileApplication>();
            services.AddScoped<IAccesslevelRolesApplication, AccesslevelRolesApplication>();
            services.AddScoped<IAddressApplication, AddressApplication>();
            services.AddScoped<ICountryApplication, CountryApplication>();
            services.AddScoped<IProvinceApplication, ProvinceApplication>();
            services.AddScoped<ICityApplication, CityApplication>();
            services.AddScoped<ICategoryApplication, CategoryApplication>();
            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<IKeywordApplication, KeywordApplication>();
            services.AddScoped<IKeywordProductsApplication, KeywordProductsApplication>();
            services.AddScoped<IProductPriceApplication, ProductPriceApplication>();
            services.AddScoped<IProductMediaApplication, ProductMediaApplication>();
            services.AddScoped<IProductReviewsApplication, ProductReviewsApplication>();
            services.AddScoped<IProductReviewsLikeApplication, ProductReviewsLikeApplication>();
            services.AddScoped<IProductAskApplication, ProductAskApplication>();
            services.AddScoped<IProductAskLikesApplication, ProductAskLikesApplication>();
            services.AddScoped<IProductReviewsMediaApplication, ProductReviewsMediaApplication>();
            services.AddScoped<IProductTopicApplication, ProductTopicApplication>();
            services.AddScoped<IProductPropertisApplication, ProductPropertisApplication>();
            services.AddScoped<IProductPropertiesValuesApplication, ProductPropertiesValuesApplication>();
            services.AddScoped<IProductVariantsApplication, ProductVariantsApplication>();
            services.AddScoped<IProductVariantItemsApplication, ProductVariantItemsApplication>();
            services.AddScoped<IProductSellersApplication, ProductSellersApplication>();
            services.AddScoped<IProductReviewsAttributeApplication, ProductReviewsAttributeApplication>();
            services.AddScoped<IProductReviewsAttributeValuesApplication, ProductReviewsAttributeValuesApplication>();
            services.AddScoped<ICurrencyApplication, CurrencyApplication>();
            services.AddScoped<IFileTypeApplication, FileTypeApplication>();
            services.AddScoped<IFilePathApplication, FilePathApplication>();
            services.AddScoped<IGuaranteeApplications, GuaranteeApplications>();
            services.AddScoped<IPostingRestrictionsApplication, PostingRestrictionsApplication>();
            services.AddScoped<ISellerApplication, SellerApplication>();
            services.AddScoped<IProductDiscountApplication, ProductDiscountApplication>();
            services.AddScoped<ISliderApplication, SliderApplication>();
            services.AddScoped<IShowcaseApplication, ShowcaseApplication>();
            services.AddScoped<IShowcaseTabApplication, ShowcaseTabApplication>();
            services.AddScoped<IShowcaseTabSectionApplication, ShowcaseTabSectionApplication>();
            services.AddScoped<ISectionFreeItemApplication, SectionFreeItemApplication>();
            services.AddScoped<ISectionProductApplication, SectionProductApplication>();
            services.AddScoped<ISectionProductCategoryApplication, SectionProductCategoryApplication>();
            services.AddScoped<ISectionProductKeywordApplication, SectionProductKeywordApplication>();
            services.AddScoped<IShowcaseTabSectionItemApplication, ShowcaseTabSectionItemApplication>();
            services.AddScoped<ISearchHistoryApplication, SearchHistoryApplication>();
            services.AddScoped<ICartApplication, CartApplication>();
            services.AddScoped<IPaymentGateApplication, PaymentGateApplication>();
            services.AddScoped<IPaymentGateRestrictApplication, PaymentGateRestrictApplication>();
            services.AddScoped<IShippingMethodApplication, ShippingMethodApplication>();
            services.AddScoped<IShippingMethodRestrictApplication, ShippingMethodRestrictApplication>();
            services.AddScoped<IBillApplication, BillApplication>();
            services.AddScoped<IBillItemsApplication, BillItemsApplication>();
            services.AddScoped<IPostalBarcodeApplication, PostalBarcodeApplication>();
            services.AddScoped<ITaxGroupApplication, TaxGroupApplication>();
            services.AddScoped<IProductGroupApplication, ProductGroupApplication>();
            services.AddScoped<IProductGroupPercentApplication, ProductGroupPercentApplication>();
            services.AddScoped<IWalletApplication, WalletApplication>();
            services.AddScoped<IWalletDetailsApplication, WalletDetailsApplication>();
            services.AddScoped<IWalletProductDepositDetailsApplication, WalletProductDepositDetailsApplication>();
            services.AddScoped<IWalletWithdrawApplication, WalletWithdrawApplication>();
            services.AddScoped<IBankAccountApplication, BankAccountApplication>();
        }
    }
}
