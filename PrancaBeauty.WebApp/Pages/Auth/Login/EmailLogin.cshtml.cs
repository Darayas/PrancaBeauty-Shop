using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Localization;

namespace PrancaBeauty.WebApp.Pages.Auth.Login
{
    public class EmailLoginModel : PageModel
    {
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        public EmailLoginModel(IUserApplication userApplication, ILocalizer localizer)
        {
            _UserApplication = userApplication;
            _Localizer = localizer;
        }

        public async Task<IActionResult> OnGetAsync(string Token)
        {
            if (!string.IsNullOrWhiteSpace(Token))
                return StatusCode(400);

            string DecryptedToken = Token.AesDecrypt(AuthConst.SecretKey);
            string UserId = DecryptedToken.Split(", ")[0];
            string Password = DecryptedToken.Split(", ")[1];

            var Result = await _UserApplication.LoginByEmailLinkStep2Async(UserId, Password);
            if (Result.IsSucceeded)
            {

            }
            else
            {
                ViewData["Message"] = _Localizer[Result.Message];
                ViewData["MsgType"] = "Faild";
            }
        }
    }
}
