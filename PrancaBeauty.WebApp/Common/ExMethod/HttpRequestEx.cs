using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.ExMethod
{
    public static class HttpRequestEx
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            return (request.Headers["X-Requested-With"] == "XMLHttpRequest") || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest"));
        }

        public static string GetCurrentUrl(this HttpRequest request)
        {
            string Url = request.Scheme + "://" + request.Host + request.Path + request.QueryString;
            if (request.QueryString.HasValue)
                Url += request.QueryString.Value;

            return Url;
        }

        public static string GetCurrentUrlEncoded(this HttpRequest request)
        {
            string Url = request.Scheme + "://" + request.Host + request.Path + request.QueryString;
            if (request.QueryString.HasValue)
                Url += request.QueryString.Value;

            return WebUtility.UrlEncode(Url);
        }
    }
}
