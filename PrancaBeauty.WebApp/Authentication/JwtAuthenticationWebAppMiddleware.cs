using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Authentication
{
    public class JwtAuthenticationWebAppMiddleware
    {
        private readonly RequestDelegate _Next;
        public JwtAuthenticationWebAppMiddleware(RequestDelegate Next)
        {
            _Next = Next;
        }

        public async Task InvokeAsync(HttpContext context)
        {



            await _Next(context);
        }
    }
}
