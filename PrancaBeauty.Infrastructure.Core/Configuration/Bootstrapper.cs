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
using PrancaBeauty.Application.Apps.Categories;
using PrancaBeauty.Application.Apps.Cities;
using PrancaBeauty.Application.Apps.Countries;
using PrancaBeauty.Application.Apps.Files;
using PrancaBeauty.Application.Apps.FileServer;
using PrancaBeauty.Application.Apps.Keywords;
using PrancaBeauty.Application.Apps.KeywordsProducts;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.ProductAsk;
using PrancaBeauty.Application.Apps.ProductAskLikes;
using PrancaBeauty.Application.Apps.ProductMedia;
using PrancaBeauty.Application.Apps.ProductPrices;
using PrancaBeauty.Application.Apps.ProductPropertiesValues;
using PrancaBeauty.Application.Apps.ProductPropertis;
using PrancaBeauty.Application.Apps.ProductReviews;
using PrancaBeauty.Application.Apps.ProductReviewsLike;
using PrancaBeauty.Application.Apps.ProductReviewsMedia;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Apps.ProductTopic;
using PrancaBeauty.Application.Apps.ProductVariants;
using PrancaBeauty.Application.Apps.Province;
using PrancaBeauty.Application.Apps.Roles;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Apps.Templates;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Common.FtpWapper;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Contracts;
using PrancaBeauty.Domin.FileServer.FileAgg.Contracts;
using PrancaBeauty.Domin.FileServer.ServerAgg.Contracts;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Contracts;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Contracts;
using PrancaBeauty.Domin.Product.ProductAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductAskAgg.Contarcts;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductMediaAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPropertisAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductReviewsMediaAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductVariantAgg.Contracts;
using PrancaBeauty.Domin.Region.CityAgg.Contracts;
using PrancaBeauty.Domin.Region.CountryAgg.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Contracts;
using PrancaBeauty.Domin.Region.ProvinceAgg.Contracts;
using PrancaBeauty.Domin.Settings.SettingsAgg.Contracts;
using PrancaBeauty.Domin.Templates.TemplatesAgg.Contracts;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Contracts;
using PrancaBeauty.Domin.Users.AddressAgg.Contracts;
using PrancaBeauty.Domin.Users.RoleAgg.Contracts;
using PrancaBeauty.Domin.Users.UserAgg.Contracts;
using PrancaBeauty.Infrastructure.EFCore.Context;
using PrancaBeauty.Infrastructure.EFCore.Repository.AccessLevel;
using PrancaBeauty.Infrastructure.EFCore.Repository.Address;
using PrancaBeauty.Infrastructure.EFCore.Repository.Categories;
using PrancaBeauty.Infrastructure.EFCore.Repository.Cities;
using PrancaBeauty.Infrastructure.EFCore.Repository.Counties;
using PrancaBeauty.Infrastructure.EFCore.Repository.FileServer;
using PrancaBeauty.Infrastructure.EFCore.Repository.Keywords;
using PrancaBeauty.Infrastructure.EFCore.Repository.Keywords_Products;
using PrancaBeauty.Infrastructure.EFCore.Repository.Product;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductAsk;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductAskLikes;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductMedia;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductPrices;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductProperties;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductPropertiesValues;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviews;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsLike;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductReviewsMedia;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductTopic;
using PrancaBeauty.Infrastructure.EFCore.Repository.ProductVariants;
using PrancaBeauty.Infrastructure.EFCore.Repository.Province;
using PrancaBeauty.Infrastructure.EFCore.Repository.Region;
using PrancaBeauty.Infrastructure.EFCore.Repository.Roles;
using PrancaBeauty.Infrastructure.EFCore.Repository.Settings;
using PrancaBeauty.Infrastructure.EFCore.Repository.Templates;
using PrancaBeauty.Infrastructure.EFCore.Repository.Users;
using PrancaBeauty.Infrastructure.Logger.Serilogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.Core.Configuration
{
    public static class Bootstrapper
    {
        public static void Config(this IServiceCollection services)
        {
            services.AddDbContext<MainContext>(opt => opt.UseSqlServer("Server=.;Database=PrancaBeautyDb;Trusted_Connection=True;"));

            services.AddScoped<ILogger, Serilogger>();
            services.AddScoped<IEmailSender, GmailSender>();
            services.AddScoped<ISmsSender, KaveNegarSmsSender>();
            services.AddScoped<IDownloader, Downloader>();
            services.AddScoped<IIPList, IPList>();
            services.AddScoped<IAniShell, AniShell>();
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
        }
    }
}
