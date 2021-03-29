using Microsoft.AspNetCore.Http;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.WebApp.Common.ExMethod;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UAParser;

namespace PrancaBeauty.WebApp.Middlewares
{
    public class RedirectWhenNotRobotsMiddleware
    {
        private readonly RequestDelegate _Next;
        public RedirectWhenNotRobotsMiddleware(RequestDelegate next)
        {
            _Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //if (context.Request.GetCurrentUrl().Trim('/').ToLower().EndsWith("/robotindex"))
            if (context.Request.Path == "/")
            {
                var _SettingApplication = (ISettingApplication)context.RequestServices.GetService(typeof(ISettingApplication));
                var _LanguageApplication = (ILanguageApplication)context.RequestServices.GetService(typeof(ILanguageApplication));

                ClientInfo clientInfo = Parser.GetDefault().Parse(context.Request.Headers["User-Agent"].ToString());
                if (!clientInfo.Device.IsSpider)
                {
                    string SiteUrl = (await _SettingApplication.GetSettingAsync(CultureInfo.CurrentCulture.Name)).SiteUrl;
                    string LangAbbr = await _LanguageApplication.GetAbbrByCodeAsync(CultureInfo.CurrentCulture.Name);

                    context.Response.Redirect(SiteUrl + "/" + LangAbbr);
                }
            }

            await _Next(context);
        }
    }
}
