using Microsoft.AspNetCore.Http;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Languages;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Settings;
using PrancaBeauty.WebApp.Common.Utility.IpAddress;
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

                    var isValid = await _LanguageApplication.IsValidAbbrForSiteLangAsync(new InpIsValidAbbrForSiteLang { Abbr = LangAbbr });
                    if (!isValid)
                    {
                        var _SiteSetting = await _SettingApplication.GetSettingAsync(new InpGetSetting { LangCode = CultureInfo.CurrentCulture.Name });
                        string SiteUrl = "";

                        if (_SiteSetting == null)
                            SiteUrl = context.Request.Scheme + "://" + context.Request.Host;
                        else
                            SiteUrl = _SiteSetting.SiteUrl;


                        Paths[0] = GetLangByIpAddress(context);

                        context.Response.StatusCode = 301;
                        context.Response.Redirect(SiteUrl + "/" + string.Join("/", Paths));
                    }
                }
                else
                {
                    var _SiteSetting = await _SettingApplication.GetSettingAsync(new InpGetSetting { LangCode = CultureInfo.CurrentCulture.Name });
                    string SiteUrl = "";

                    if (_SiteSetting == null)
                        SiteUrl = context.Request.Scheme + "://" + context.Request.Host;
                    else
                        SiteUrl = _SiteSetting.SiteUrl;

                    string _LangAbbr = GetLangByIpAddress(context);

                    context.Response.Redirect(SiteUrl + $"/{_LangAbbr}");
                }
            }

            await _Next(context);
        }

        private string GetLangByIpAddress(HttpContext context)
        {
            var _IpAddressChecker = (IIpAddressChecker)context.RequestServices.GetService(typeof(IIpAddressChecker));

            var _LangAbbrByIpAddress = _IpAddressChecker.GetLangAbbr(context.Connection.RemoteIpAddress.ToString());

            if (_LangAbbrByIpAddress == null)
                return "fa";
            else
                return _LangAbbrByIpAddress;
        }
    }
}
