using AspNetCoreRateLimit;
using Framework.Application.Consts;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using PrancaBeauty.Infrastructure.Core.Configuration;
using PrancaBeauty.Infrastructure.Logger.Serilog;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Config;
using PrancaBeauty.WebApp.Localization.Resource;
using PrancaBeauty.WebApp.Middlewares;
using System;
using System.Collections.Generic;
using System.Globalization;
using WebMarkupMin.AspNetCore5;

var builder = WebApplication.CreateBuilder(args);
WebApplication app = null;

#region Configure services
{
    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
    {
        builder.Host.UseSeriLog_Console();
    }
    else
    {
        builder.Host.UseSeriLog_SqlServer();
    }

    builder.Services.AddLocalization("Localization/Resource");

    builder.Services.GZipConfig();

    builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

    builder.Services.WebEncoderConfig();

    builder.Services.AddCustomAuthorization();

    builder.Services.Config();

    builder.Services.AddInject();

    builder.Services.AddRazorPage()
            .AddFilters()
            .AddCustomViewLocalization("Localization/Resource")
            .AddCustomDataAnnotationLocalization(builder.Services, typeof(SharedResource))
            .AddNewtonsoftJson(opt => opt.SerializerSettings.ContractResolver = new DefaultContractResolver());

    builder.Services.AddHttpContextAccessor();

    builder.Services.AddCustomWebMarkupMin();

    builder.Services.Config();

    builder.Services.AddInject();

    builder.Services.RateLimitConfig(builder.Configuration);

    builder.Services.AddCustomIdentity()
            .AddErrorDescriber<CustomErrorDescriber>();

    builder.Services.AddJwtAuthentication(AuthConst.SecretCode, AuthConst.SecretKey, AuthConst.Audience, AuthConst.Issuer);

    builder.Services.AddKendo();

    builder.Services.AddAutoMapper(typeof(Program));
}
#endregion

#region Configure
{
    app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseCustomCaching();

    app.RedirectStatusCode();

    app.UseResponseCompression();

    app.UseWebMarkupMin();

    app.UseRouting();

    app.UseStaticFiles();

    app.UseLocalization(new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("fa-IR") }, "fa-ir");

    app.UseJwtAuthentication(AuthConst.CookieName);

    app.UseMiddleware<NeedToRebuildTokenMiddleware>();

    app.UseRedirectNotRobots();

    app.UseMiddleware<RedirectToValidLangMiddleware>();

    app.UseIpRateLimiting();


    app.UseEndpoints(endpoints =>
    {
        endpoints.MapRazorPages();
    });
}
#endregion

#region Use services
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var Services = serviceScope.ServiceProvider;
        try
        {

        }
        catch (Exception ex)
        {
            var logger = Services.GetRequiredService<ILogger>();
            logger.Error(ex);
        }
    }
}
#endregion

await app.RunAsync();