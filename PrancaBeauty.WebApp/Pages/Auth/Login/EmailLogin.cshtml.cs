using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Localization;

namespace PrancaBeauty.WebApp.Pages.Auth.Login
{
    public class EmailLoginModel : PageModel
    {
        private readonly ILocalizer _Localizer;
        private readonly IJWTBuilder _JWTBuilder;
        private readonly IUserApplication _UserApplication;
        public EmailLoginModel(IUserApplication userApplication, ILocalizer localizer, IJWTBuilder jWTBuilder)
        {
            _UserApplication = userApplication;
            _Localizer = localizer;
            _JWTBuilder = jWTBuilder;
        }

        public async Task<IActionResult> OnGetAsync(string Token)
        {
            if (!string.IsNullOrWhiteSpace(Token))
                return StatusCode(400);

            string DecryptedToken = Token.AesDecrypt(AuthConst.SecretKey);
            string UserId = DecryptedToken.Split(", ")[0];
            string Password = DecryptedToken.Split(", ")[1];
            string Ip = DecryptedToken.Split(", ")[2];
            string Date = DecryptedToken.Split(", ")[3];
            bool RemmeberMe = bool.Parse(DecryptedToken.Split(", ")[4]);

            var Result = await _UserApplication.LoginByEmailLinkStep2Async(UserId, Password, Ip, HttpContext.Connection.RemoteIpAddress.ToString(), DateTime.Parse(Date));
            if (Result.IsSucceeded)
            {
                string GeneratedToken = await _JWTBuilder.CreateTokenAync(Result.Message);
                Response.CreateAuthCookie(GeneratedToken, RemmeberMe);

                ViewData["Message"] = _Localizer["LoginEmailSuccess"];
                ViewData["MsgType"] = "success";
                return Page();
            }
            else
            {
                ViewData["Message"] = _Localizer[Result.Message];
                ViewData["MsgType"] = "danger";
                return Page();
            }
        }
    }
}
