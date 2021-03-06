using Framework.Application.Consts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Common.ExMethod
{
    public static class HttpResponseEx
    {
        public static void CreateAuthCookie(this HttpResponse response, string AuthToken, bool RemmemberMe)
        {
            // حذف کوکی
            response.Cookies.Delete(AuthConst.CookieName);

            // ایجاد کوکی
            response.Cookies.Append(AuthConst.CookieName, AuthToken, RemmemberMe ? new CookieOptions() { Expires = DateTime.Now.AddHours(48) } : new CookieOptions());
        }
    }
}
