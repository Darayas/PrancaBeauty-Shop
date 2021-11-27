using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.EditProfile.Components.ChangePassword
{
    [Authorize]
    public class Compo_ChangePasswordModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        public Compo_ChangePasswordModel(IUserApplication userApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _UserApplication = userApplication;
            _MsgBox = msgBox;
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

            string UserId = User.GetUserDetails().UserId;

            var Result = await _UserApplication.ChanagePasswordAsync(new InpChanagePassword
            {
                UserId = User.GetUserDetails().UserId,
                CurrentPassword = Input.CurrentPassword,
                NewPassword = Input.ConfirmPassword
            });

            if (Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[Result.Message]);
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message].Replace(", ", "<br/>"));
            }
        }

        [BindProperty]
        public viCompo_ChangePassword Input { get; set; }

    }
}
