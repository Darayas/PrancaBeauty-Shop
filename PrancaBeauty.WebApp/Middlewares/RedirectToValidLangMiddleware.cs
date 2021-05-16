using Microsoft.AspNetCore.Http;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Middlewares
{
    public class RedirectToValidLangMiddleware
    {
        private readonly RequestDelegate _Next;
        public RedirectToValidLangMiddleware(RequestDelegate next)
        {
            _Next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // GET فقط برای حالت 
            if (context.Request.Method.ToLower() == "get")
            {
                string[] Paths = context.Request.Path.HasValue ? context.Request.Path.Value.Trim(new char[] { '/' }).Split("/") : new string[] { };

                var _SettingApplication = (ISettingApplication)context.RequestServices.GetService(typeof(ISettingApplication));
                var _LanguageApplication = (ILanguageApplication)context.RequestServices.GetService(typeof(ILanguageApplication));

                if (Paths.Any())
                {
                    // زبان انتخاب شده
                    string LangAbbr = Paths.First();

                    var isValid = await _LanguageApplication.IsValidAbbrForSiteLangAsync(LangAbbr);
                    if (!isValid)
                    {
                        string SiteUrl = (await _SettingApplication.GetSettingAsync(CultureInfo.CurrentCulture.Name)).SiteUrl;

                        Paths[0] = "fa";

                        context.Response.StatusCode = 301;
                        context.Response.Redirect(SiteUrl + "/" + string.Join("/", Paths));
                    }
                }
                else
                {
                    string SiteUrl = (await _SettingApplication.GetSettingAsync(CultureInfo.CurrentCulture.Name)).SiteUrl;
                    context.Response.Redirect(SiteUrl + "/fa");
                }
            }

            await _Next(context);
        }
    }
}
