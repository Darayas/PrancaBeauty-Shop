using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using PrancaBeauty.WebApp.Config;
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

            services.WebEncoderConfig();

            services.AddRazorPage();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseLocalization(new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("fa-IR") }, "fa-IR");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
