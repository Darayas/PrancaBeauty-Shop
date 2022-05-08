﻿using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using PrancaBeauty.Application.Apps.Languages;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Languages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Fillters
{
    public class FillRegoinParametrsFilter : IAsyncPageFilter
    {
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var _LanguageApplication = (ILanguageApplication)context.HttpContext.GetService<ILanguageApplication>();
            string LangCode = CultureInfo.CurrentCulture.Name;

            var LangId = await _LanguageApplication.GetLangIdByLangCodeAsync(new InpGetLangIdByLangCode { Code= LangCode });
            var CountryId = await _LanguageApplication.GetLangIdByLangCodeAsync(new InpGetLangIdByLangCode { Code= LangCode });

            context.HandlerArguments.Add("LangId", LangId);

            await next();
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {

        }
    }
}
