using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Accesslevels;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.ApplicationDTO.AccessLevels;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Users;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Users.Components
{
    public class Compo_ChanageAccessLevelModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        private readonly IAccesslevelApplication _AccesslevelApplication;

        public Compo_ChanageAccessLevelModel(IUserApplication userApplication, IMsgBox msgBox, ILocalizer localizer, IAccesslevelApplication accesslevelApplication)
        {
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
            _AccesslevelApplication = accesslevelApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<JsonResult> OnGetReadAsync(string Text)
        {
            var qData = await _AccesslevelApplication.GetForChangeUserAccesssLevelAsync(new InpGetForChangeUserAccesssLevel { Name = Text });
            return new JsonResult(qData);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var Result = await _UserApplication.ChanageUserAccessLevelAsync(new InpChanageUserAccessLevel
            {
                UserId = Input.UserId,
                SelfUserId = User.GetUserDetails().UserId,
                AccessLevelId = Input.AccessLevelId
            });

            if (Result.IsSucceeded)
            {
                CacheUsersToRebuildToken.Add(Input.UserId);

                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "Close(); RefreshData();");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ChanageAccessLevel Input { get; set; }
    }
}
