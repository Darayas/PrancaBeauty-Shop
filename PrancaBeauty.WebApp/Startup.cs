using Framework.Application.Consts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using PrancaBeauty.Infrastructure.Core.Configuration;
using PrancaBeauty.Infrastructure.EFCore.Data;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Config;
using PrancaBeauty.WebApp.Localization.Resource;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization("Localization/Resource");

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.WebEncoderConfig();

            services.AddRazorPage()
                    .AddCustomViewLocalization("Localization/Resource")
                    .AddCustomDataAnnotationLocalization(services, typeof(SharedResource));

            services.Config();

            services.AddInject();

            services.AddCustomIdentity()
                    .AddErrorDescriber<CustomErrorDescriber>();

            services.AddJwtAuthentication(AuthConst.SecretCode, AuthConst.SecretKey, AuthConst.Audience, AuthConst.Issuer);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseLocalization(new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("fa-IR") }, "fa-ir");

            app.UserJwtAuthentication(AuthConst.CookieName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
