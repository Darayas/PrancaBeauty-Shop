using Framework.Application.Consts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.WebApp.Common.ExMethod
{
    public static class HttpResponseEx
    {
        public static void CreateAuthCookie(this HttpResponse response, string AuthToken, bool RemmemberMe)
        {
            // حذف کوکی های اصلی و کوکی هایی که ایدنتیتی بصورت خودکار ایجاد میکند
            #region Remove cookies
            {
                response.Cookies.Delete(".AspNetCore.Identity.Application");

                for (int i = 1; i <= 10; i++)
                {
                    response.Cookies.Delete(AuthConst.CookieName + i);
                    response.Cookies.Delete(".AspNetCore.Identity.ApplicationC" + i);
                }
            }
            #endregion

            // ایجاد و شماره گذاری کوکی ها برای زمانی که توکن بیش از 2000 کاراکتر باشد
            // محدودیت حجمی کوکی ها در مرورگرهای مختلف متفاوت است به همین خاطر حداقل را در نظر گرفته ایم
            // میدلور مربوط به احراز هویت را مشاهده نمایید
            #region Add new cookies
            {
                int i = 0;
                var LstCookieItems = new List<string>();
                while (AuthToken != null)
                {
                    if (AuthToken.Length > 2000)
                    {
                        string _Section = AuthToken.Substring(0, 2000);

                        response.Cookies.Append(AuthConst.CookieName + i, _Section, RemmemberMe ? new CookieOptions() { Expires = DateTime.Now.AddHours(48) } : new CookieOptions());

                        AuthToken = AuthToken.Remove(0, 2000);
                    }
                    else
                    {
                        response.Cookies.Append(AuthConst.CookieName + i, AuthToken, RemmemberMe ? new CookieOptions() { Expires = DateTime.Now.AddHours(48) } : new CookieOptions());
                        AuthToken = null;
                    }

                    i++;
                }
            }
            #endregion


            /*
             * دلیل تغییرات بالا، زیاد شدن حجم رکوست ها میباشد که منجر به ایجاد کد خطای 400 میگردد
             * و همچنین به علت ایجاد رول های متعدد، تعداد کاراکترهای توکن بیش از 2000 کاراکتر یا 2 کیلوبایت میشود و این نیز باعث ایجاد خطاهای بعدی میگردد
             */
        }
    }
}
