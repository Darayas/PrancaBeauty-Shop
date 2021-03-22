using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using PrancaBeauty.Application.Apps.Languages;
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

            var LangCode = await _LanguageApplication.GetCodeByAbbrAsync(CultureName);
            if (LangCode == null)
                LangCode = "fa-IR";

            return new ProviderCultureResult(LangCode, LangCode);
        }
    }
}
