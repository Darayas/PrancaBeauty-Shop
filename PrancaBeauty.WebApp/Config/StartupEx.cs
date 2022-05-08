using AspNetCoreRateLimit;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.IpAddress;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Fillters;
using PrancaBeauty.WebApp.Localization;
using PrancaBeauty.WebApp.Middlewares;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebMarkupMin.AspNetCore5;
using WebMarkupMin.Core;

namespace PrancaBeauty.WebApp.Config
{
    public static class StartupEx
    {
        public static IServiceCollection WebEncoderConfig(this IServiceCollection services)
        {
            return services.Configure<WebEncoderOptions>(opt =>
            {
                opt.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.Arabic, UnicodeRanges.BasicLatin);
            });
        }

        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            return services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminPanelPolicy", pol => pol.RequireRole(new string[] { "AdminPage" }));
            });
        }

        public static IMvcBuilder AddRazorPage(this IServiceCollection services)
        {
            return services.AddRazorPages(a =>
            {
                a.Conventions.AddPageRoute("/Home/RobotIndex", "");
                a.Conventions.AuthorizeFolder("/Admin/", "AdminPanelPolicy");
            });
        }

        public static IMvcBuilder AddFilters(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddMvcOptions(opt =>
            {
                opt.Filters.Add(new FillRegoinParametrsFilter());
            });
        }

        public static IServiceCollection AddLocalization(this IServiceCollection services, string ResourcePath)
        {
            return services.AddLocalization(a => a.ResourcesPath = ResourcePath);
        }

        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app, List<CultureInfo> SupportedLans, string DefCultureName = "fa-IR")
        {
            var Options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(DefCultureName),
                SupportedCultures = SupportedLans,
                SupportedUICultures = SupportedLans,
                RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    //new CookieRequestCultureProvider(),
                    //new QueryStringRequestCultureProvider()
                    new PathRequestCultureProvider()
                }
            };

            return app.UseRequestLocalization(Options);
        }

        public static IMvcBuilder AddCustomViewLocalization(this IMvcBuilder builder, string ResourcePath)
        {
            builder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, option =>
            {
                option.ResourcesPath = ResourcePath;
            });

            return builder;
        }

        public static IMvcBuilder AddCustomDataAnnotationLocalization(this IMvcBuilder builder, IServiceCollection services, Type SharedResource)
        {
            builder.AddDataAnnotationsLocalization(Options =>
            {
                var Localizer = new FactoryLocalizer().Set(services, SharedResource);

                Options.DataAnnotationLocalizerProvider = (t, f) => Localizer;
            });

            return builder;
        }

        public static IServiceCollection AddInject(this IServiceCollection services)
        {
            services.AddSingleton<ILocalizer, Localizer>();
            services.AddSingleton<IMsgBox, MsgBox>();
            services.AddScoped<IJWTBuilder, JWTBuilder>();
            services.AddSingleton<IIpAddressChecker, IpAddressChecker>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            return services;
        }

        public static IApplicationBuilder UseRedirectNotRobots(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RedirectWhenNotRobotsMiddleware>();
        }

        public static IApplicationBuilder RedirectStatusCode(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                await next.Invoke();

                if (context.Response.StatusCode == 401)
                {
                    var rqf = context.Request.HttpContext.Features.Get<IRequestCultureFeature>();
                    string culture = rqf.RequestCulture.Culture.Parent.Name;

                    context.Response.Redirect($"/{culture}/Auth/Login");
                }

                //if (context.Response.StatusCode == 429)
                //{
                //    var rqf = context.Request.HttpContext.Features.Get<IRequestCultureFeature>();
                //    string culture = rqf.RequestCulture.Culture.Parent.Name;

                //    context.Response.Redirect($"/{culture}/Error/429");
                //}

                if (context.Response.StatusCode == 404)
                {
                    var rqf = context.Request.HttpContext.Features.Get<IRequestCultureFeature>();
                    string culture = rqf.RequestCulture.Culture.Parent.Name;

                    context.Response.Redirect($"/{culture}/Error/404");
                }

                if (context.Response.StatusCode == 400)
                {
                    var rqf = context.Request.HttpContext.Features.Get<IRequestCultureFeature>();
                    string culture = rqf.RequestCulture.Culture.Parent.Name;

                    context.Response.Redirect($"/{culture}/Error/400");
                }
            });
        }

        public static IServiceCollection RateLimitConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

            services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            return services;
        }

        public static void AddCustomWebMarkupMin(this IServiceCollection services)
        {
            services.AddWebMarkupMin(opt =>
            {
                opt.AllowMinificationInDevelopmentEnvironment = true;
                opt.AllowCompressionInDevelopmentEnvironment = true;
            }).AddHtmlMinification(opt =>
            {
                opt.MinificationSettings.WhitespaceMinificationMode = WhitespaceMinificationMode.None;
            });
        }

        public static IServiceCollection GZipConfig(this IServiceCollection services)
        {
            services.Configure<BrotliCompressionProviderOptions>(opt =>
            {
                opt.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(opt =>
            {
                opt.EnableForHttps = true;
                opt.MimeTypes = new List<string> {
                    "application/javascript",
                    "application/rss+xml",
                    "application/vnd.ms-fontobject",
                    "application/x-font",
                    "application/x-font-opentype",
                    "application/x-font-otf",
                    "application/x-font-truetype",
                    "application/x-font-ttf",
                    "application/x-javascript",
                    "application/xhtml+xml",
                    "application/xml",
                    "font/opentype",
                    "font/otf",
                    "font/ttf",
                    "image/svg+xml",
                    "image/x-icon",
                    "text/css",
                    "text/html",
                    "text/javascript",
                    "text/plain",
                    "text/xml"
                };
                opt.Providers.Add<BrotliCompressionProvider>();
            });

            return services;
        }

        public static IApplicationBuilder UseCustomCaching(this IApplicationBuilder application)
        {
            return application.Use(async (context, next) =>
            {
                string path = context.Request.Path;

                // css, js
                if (path.EndsWith(".css") || path.EndsWith(".js"))
                {
                    TimeSpan maxAge = new TimeSpan(365, 0, 0, 0);
                    context.Request.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
                }

                // fonts
                else if (path.EndsWith(".woff") || path.EndsWith(".woff2") || path.EndsWith(".tff") || path.EndsWith(".svg") || path.EndsWith(".eot"))
                {
                    TimeSpan maxAge = new TimeSpan(365, 0, 0, 0);
                    context.Request.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
                }

                // Directory
                else if (path.Contains("/lib/"))
                {
                    TimeSpan maxAge = new TimeSpan(365, 0, 0, 0);
                    context.Request.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
                }

                // Images
                else if (path.EndsWith(".png") || path.EndsWith(".jpg") || path.EndsWith(".jpeg") || path.EndsWith(".bmp") || path.EndsWith(".gif"))
                {
                    TimeSpan maxAge = new TimeSpan(365, 0, 0, 0);
                    context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
                }

                // View
                else
                {
                    context.Response.Headers.Append("Cache-Control", "no-cache");
                    context.Response.Headers.Append("Cache-Control", "private, no-store");
                }

                await next();
            });
        }
    }
}
