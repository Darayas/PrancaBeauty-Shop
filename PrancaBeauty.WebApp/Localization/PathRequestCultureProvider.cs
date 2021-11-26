using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Contracts.Languages;
using PrancaBeauty.WebApp.Common.Utility.IpAddress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Localization
{
    public class PathRequestCultureProvider : RequestCultureProvider
    {
        public override async Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException();

            var _LanguageApplication = (ILanguageApplication)httpContext.RequestServices.GetService(typeof(ILanguageApplication));

            string Path = httpContext.Request.Path;
            string CultureName = Path.Trim('/').Split("/")[0];

            var LangCode = await _LanguageApplication.GetCodeByAbbrAsync(new InpGetCodeByAbbr { Abbr= CultureName });
            if (LangCode == null)
            {
                //httpContext.Response.Redirect("/fa");
                //return new ProviderCultureResult("fa-IR", "fa-IR");

                var _IpAddressChecker = (IIpAddressChecker)httpContext.RequestServices.GetService(typeof(IIpAddressChecker));
                string _IpAddress = httpContext.Connection.RemoteIpAddress.ToString();

                if (_IpAddressChecker.CheckIp(_IpAddress) == "ir")
                {
                    LangCode = "fa-IR";
                }
                else if (_IpAddressChecker.CheckIp(_IpAddress) == "us")
                {
                    LangCode = "en-US";
                }
                else
                {
                    LangCode = "fa-IR";
                }
            }

            return new ProviderCultureResult(LangCode, LangCode);
        }
    }
}
