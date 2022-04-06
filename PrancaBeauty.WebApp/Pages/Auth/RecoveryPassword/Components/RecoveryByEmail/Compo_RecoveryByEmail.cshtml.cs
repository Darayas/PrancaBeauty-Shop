using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Settings;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.RecoveryPassword.Components.RecoveryByEmail
{
    public class Compo_RecoveryByEmailModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        private readonly ISettingApplication _SettingApplication;
        public Compo_RecoveryByEmailModel(IUserApplication userApplication, IMsgBox msgBox, ISettingApplication settingApplication, ILocalizer localizer)
        {
            _UserApplication = userApplication;
            _MsgBox = msgBox;
            _SettingApplication = settingApplication;
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

            string _Url = (await _SettingApplication.GetSettingAsync(new InpGetSetting { LangCode = CultureInfo.CurrentCulture.Name })).SiteUrl + $"/{CultureInfo.CurrentCulture.Parent.Name}/Auth/RecoveryPassword?Token=[Token]";

            var Result = await _UserApplication.RecoveryPasswordByEmailStep1Async(new InpRecoveryPasswordByEmailStep1 { Email = Input.Email, ResetLinkTemplate = _Url });
            if (Result.IsSucceeded)
                return _MsgBox.SuccessMsg(_Localizer[Result.Message]);
            else
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
        }

        [BindProperty]
        public viCompo_RecoveryByEmail Input { get; set; }
    }
}
