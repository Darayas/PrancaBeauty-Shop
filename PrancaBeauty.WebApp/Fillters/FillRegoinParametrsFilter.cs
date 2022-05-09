using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Languages;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Fillters
{
    public class FillRegoinParametrsFilter : IAsyncPageFilter
    {
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var _LanguageApplication = context.HttpContext.GetService<ILanguageApplication>();
            string LangCode = CultureInfo.CurrentCulture.Name;

            var LangId = await _LanguageApplication.GetLangIdByLangCodeAsync(new InpGetLangIdByLangCode { Code= LangCode });
            var CountryId = await _LanguageApplication.GetCountryIdByLangCodeAsync(new InpGetCountryIdByLangCode { Code= LangCode });
            var CurrencyId = await _LanguageApplication.GetCurrencyIdByLangCodeAsync(new InpGetCurrencyIdByLangCode { Code= LangCode });

            context.HandlerArguments.Add("LangId", LangId);
            context.HandlerArguments.Add("CountryId", CountryId);
            context.HandlerArguments.Add("CurrencyId", CurrencyId);

            await next();
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {

        }
    }
}
