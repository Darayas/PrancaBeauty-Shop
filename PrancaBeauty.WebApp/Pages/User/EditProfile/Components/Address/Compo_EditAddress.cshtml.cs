using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Address;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.EditProfile.Components.Address
{
    public class Compo_EditAddressModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IAddressApplication _AddressApplication;
        public Compo_EditAddressModel(ILocalizer localizer, IMsgBox msgBox, IAddressApplication addressApplication, IMapper mapper)
        {
            _Localizer = localizer;
            _MsgBox = msgBox;
            _AddressApplication = addressApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_EditAddress Input { get; set; }
    }
}
