using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.EditProfile
{
    public class EditProfileModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IUserApplication _UserApplication;
        public EditProfileModel(IUserApplication userApplication, IMsgBox msgBox)
        {
            _UserApplication = userApplication;
            _MsgBox = msgBox;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public viEditProfile Input { get; set; }
    }
}
