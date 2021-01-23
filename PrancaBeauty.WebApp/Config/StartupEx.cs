using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

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

        public static IMvcBuilder AddRazorPage(this IServiceCollection services)
        {
            return services.AddRazorPages(a =>
            {
                a.Conventions.AddPageRoute("/Home/Index", "");
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
                    new CookieRequestCultureProvider(),
                    new QueryStringRequestCultureProvider()
                }
            };

            return app.UseRequestLocalization(Options);
        }
    }
}
