using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Accesslevels;
using PrancaBeauty.Application.Contracts.AccessLevels;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Localization;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Admin.AccessLevels
{
    [Authorize(Roles = Roles.CanAddAccessLevel)]
    public class AddModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IAccesslevelApplication _AccesslevelApplication;

        public AddModel(IAccesslevelApplication accesslevelApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _AccesslevelApplication = accesslevelApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
        }

        public IActionResult OnGet(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/AccessLevel/List";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            var Result = await _AccesslevelApplication.AddNewAsync(new InpAddNewAccessLevel()
            {
                Name = Input.Name,
                Roles = Input.Roles
            });

            if (Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "GotoList");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty]
        public viAddAccessLevel Input { get; set; }
    }
}
