using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Address;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.EditProfile.Components.Address
{
    public class Compo_AddAddressModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IAddressApplication _AddressApplication;
        public Compo_AddAddressModel(ILocalizer localizer, IMsgBox msgBox, IAddressApplication addressApplication)
        {
            _Localizer = localizer;
            _MsgBox = msgBox;
            _AddressApplication = addressApplication;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }

        [BindProperty]
        public viCompo_AddAddress Input { get; set; }
    }
}
