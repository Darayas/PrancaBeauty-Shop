using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Auth.Login.Components
{
    public class Compo_Login_EmailPasswordModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IUserApplication _UserApplication;
        private readonly IJWTBuilder _JWTBuilder;
        private readonly ILocalizer _Localizer;
        public Compo_Login_EmailPasswordModel(IUserApplication userApplication, IMsgBox msgBox, IJWTBuilder jWTBuilder, ILocalizer localizer)
        {
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _JWTBuilder = jWTBuilder;
            _Localizer = localizer;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var Result = await _UserApplication.LoginByEmailPasswordAsync(new InpLoginByEmailPassword { Email = Input.Email, Password = Input.Password });

            if (Result.IsSucceeded)
            {
                string GeneratedToken = await _JWTBuilder.CreateTokenAync(Result.Message);

                Response.CreateAuthCookie(GeneratedToken, Input.RemmeberMe);

                return new JsResult("GotoReturnUrl()");
            }
            else
            {
                return _MsgBox.InfoMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty]
        public viCompo_Login_UsernamePasswordModel Input { get; set; }
    }
}
