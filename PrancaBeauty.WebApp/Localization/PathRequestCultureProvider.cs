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
        private readonly ILanguageApplication _LanguageApplication;
        public PathRequestCultureProvider(ILanguageApplication languageApplication)
        {
            _LanguageApplication = languageApplication;
        }

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException();

            string Path = httpContext.Request.Path;
            string CultureName = Path.Trim('/').Split("/")[0];



            return Task.FromResult(new ProviderCultureResult(CultureName, CultureName));
        }
    }
}
