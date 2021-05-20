﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.ExMethod
{
    public static class HttpRequestEx
    {
        public static string GetCurrentUrl(this HttpRequest request)
        {
            string Url = request.Scheme + "://" + request.Host + request.Path;
            if (request.QueryString.HasValue)
                Url += request.QueryString.Value;

            return Url;
        }

        public static string GetCurrentUrlEncoded(this HttpRequest request)
        {
            string Url = request.Scheme + "://" + request.Host + request.Path;
            if (request.QueryString.HasValue)
                Url += request.QueryString.Value;

            return WebUtility.UrlEncode(Url);
        }
    }
}
