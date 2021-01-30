using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Common.ExMethods;

namespace PrancaBeauty.WebApp.Authentication
{
    public class JwtAuthenticationWebAppMiddleware
    {
        private string _SecretKey;
        private string _CookieName;

        private readonly RequestDelegate _Next;
        public JwtAuthenticationWebAppMiddleware(RequestDelegate Next, string SecretKey, string CookieName)
        {
            _SecretKey = SecretKey;
            _CookieName = CookieName;

            _Next = Next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies[_CookieName] != null)
            {
                string _EncrptedTokan = context.Request.Cookies[_CookieName].ToString();

                context.Request.Headers.Add("Authorization", _EncrptedTokan.AesDecrypt(_SecretKey));
            }

            await _Next(context);
        }
    }
}
