using Framework.Application.Consts;
using Microsoft.AspNetCore.Http;
using PrancaBeauty.WebApp.Common.ExMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Authentication
{
    public class NeedToRebuildTokenMiddleware
    {
        private readonly RequestDelegate _Next;
        public NeedToRebuildTokenMiddleware(RequestDelegate Next)
        {
            _Next = Next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                string UserId = context.User.GetUserDetails().UserId;
                if (CacheUsersToRebuildToken.Any(UserId))
                {
                    context.Response.Cookies.Delete(AuthConst.CookieName);

                    CacheUsersToRebuildToken.Remove(UserId);
                }
            }

            await _Next(context);
        }
    }
}
