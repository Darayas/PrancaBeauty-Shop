using Kendo.Mvc.UI.Fluent;
using PrancaBeauty.WebApp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.ExMethod
{
    public static class KendoEx
    {
        public static GridBuilder<T> DefaultSettings<T>(this GridBuilder<T> builder, ILocalizer localizer) where T : class
        {
            builder.Pageable(a =>
                   {
                       a.Messages(msg =>
                       {
                           msg.Display(localizer["PageableMsg"]);
                           msg.Empty(localizer["GridIsEmpty"]);
                           msg.ItemsPerPage(localizer["ItemsPerPage"]);
                           msg.Of(localizer["KendoOf"]);
                           msg.MorePages(localizer["MorePages"]);
                           msg.Refresh(localizer["Refresh"]);
                           msg.Previous(localizer["Previous"]);
                           msg.Next(localizer["Next"]);
                           msg.Last(localizer["Last"]);
                           msg.First(localizer["First"]);
                           msg.Page(localizer["Page"]);
                       });

                       a.AlwaysVisible(true);
                       a.ButtonCount(5);
                       a.Input(false);
                       a.PreviousNext(true);
                       a.Responsive(true);
                   });

            return builder;
        }
    }
}
