using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Auth.Login.Components
{
    public class Compo_Login_EmailLinkModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IUserApplication _UserApplication;

        public Compo_Login_EmailLinkModel(IMsgBox msgBox, IUserApplication userApplication)
        {
            _MsgBox = msgBox;
            _UserApplication = userApplication;
        }

        public IActionResult OnGet(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl ?? "/Auth/User/Index";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());


        }

        [BindProperty]
        public viCompo_Login_EmailLinkModel Input { get; set; }
    }
}
