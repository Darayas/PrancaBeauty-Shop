using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.Users;

namespace PrancaBeauty.WebApp.Pages.Auth
{
    public class EmailConfirmationModel : PageModel
    {
        private readonly IUserApplication _UserApplication;
        public EmailConfirmationModel(IUserApplication userApplication)
        {
            _UserApplication = userApplication;
        }

        public async Task<IActionResult> OnGetAsync(string Token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Token))
                    return Page();

                string DecreptedToken = Token.AesDecrypt(AuthConst.SecretKey);

                string _UserId = DecreptedToken.Split(", ")[0];
                string _Token = DecreptedToken.Split(", ")[1];


                var Result = await _UserApplication.EmailConfirmationAsync(new InpEmailConfirmation
                {
                    Token = Token,
                    UserId = _UserId
                });
                if (Result.IsSucceeded)
                {
                    return Page();
                }
                else
                {
                    return Page();
                }
            }
            catch (Exception)
            {
                return Page();
            }

        }
    }
}
