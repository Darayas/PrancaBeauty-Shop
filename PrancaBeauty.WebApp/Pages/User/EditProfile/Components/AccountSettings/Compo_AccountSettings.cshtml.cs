using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Roles;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Settings;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Users;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.EditProfile.Components.AccountSettings
{
    [Authorize]
    public class Compo_AccountSettingsModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IUserApplication _UserApplication;
        private readonly ISettingApplication _SettingApplication;
        private readonly IMapper _Mapper;
        public Compo_AccountSettingsModel(IMsgBox msgBox, IUserApplication userApplication, ILocalizer localizer, ISettingApplication settingApplication, IMapper mapper)
        {
            _MsgBox = msgBox;
            _UserApplication = userApplication;
            _Localizer = localizer;
            _SettingApplication = settingApplication;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var qData = await _UserApplication.GetUserDetailsForAccountSettingsAsync(new InpGetUserDetailsForAccountSettings { UserId = User.GetUserDetails().UserId });

            if (qData == null)
                throw new Exception();

            Input = new viCompo_AccountSettings()
            {
                LangId = qData.LangId,
                Email = qData.Email,
                FirstName = qData.FirstName,
                LastName = qData.LastName,
                PhoneNumber = qData.PhoneNumber,
                PhoneNumberConfirmed = qData.PhoneNumberConfirmed,
                BirthDate = qData.BirthDate.HasValue ? qData.BirthDate.Value.ToString("yyyy/MM/dd") : DateTime.Now.ToString("yyyy/MM/dd")
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!User.IsInRole(Roles.CanChangeProfileImage))
                    Input.ProfileImage = null;

                string SiteUrl = (await _SettingApplication.GetSettingAsync(new InpGetSetting { LangCode = CultureInfo.CurrentCulture.Name })).SiteUrl;

                var _mapData = _Mapper.Map<InpSaveAccountSettingUserDetails>(Input);
                _mapData.UserId = User.GetUserDetails().UserId;
                _mapData.UrlToChangeEmail = $"{SiteUrl}/{CultureInfo.CurrentCulture.Parent.Name}/Auth/ChangeEmail?Token=[Token]";

                var Result = await _UserApplication.SaveAccountSettingUserDetailsAsync(_mapData);
                if (Result.IsSucceeded)
                {
                    return _MsgBox.SuccessMsg(_Localizer[Result.Message]);
                }
                else
                {
                    return _MsgBox.FaildMsg(_Localizer[Result.Message]);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [BindProperty]
        public viCompo_AccountSettings Input { get; set; }
    }
}
