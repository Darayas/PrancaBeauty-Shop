using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.Login.Components
{
    public class Compo_Login_UsernamePasswordModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IUserApplication _UserApplication;
        private readonly IJWTBuilder _JWTBuilder;
        public Compo_Login_UsernamePasswordModel(IUserApplication userApplication, IMsgBox msgBox, IJWTBuilder jWTBuilder)
        {
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _JWTBuilder = jWTBuilder;
        }

        public IActionResult OnGet(string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? "/Auth/User/Index";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var Result = await _UserApplication.LoginByUserNamePasswordAsync(Input.Email, Input.Password);

            if (Result.IsSucceeded)
            {
                string GeneratedToken = await _JWTBuilder.CreateTokenAync(Result.Message);

                Response.CreateAuthCookie(GeneratedToken, Input.RemmeberMe);

                return new JsResult("GotoReturnUrl()");
            }
            else
            {
                return _MsgBox.InfoMsg(Result.Message);
            }
        }

        [BindProperty]
        public viCompo_Login_UsernamePasswordModel Input { get; set; }
    }
}
