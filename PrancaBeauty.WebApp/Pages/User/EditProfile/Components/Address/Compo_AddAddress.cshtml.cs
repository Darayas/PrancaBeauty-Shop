using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Address;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Address;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.EditProfile.Components.Address
{
    public class Compo_AddAddressModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IAddressApplication _AddressApplication;
        public Compo_AddAddressModel(ILocalizer localizer, IMsgBox msgBox, IAddressApplication addressApplication, IMapper mapper)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return _MsgBox.ModelStateMsg(ModelState.GetErrors());

            Input.UserId = User.GetUserDetails().UserId;
            var Result = await _AddressApplication.AddAddressAsync(_Mapper.Map<InpAddAddress>(Input));
            if (Result.IsSucceeded)
            {
                return _MsgBox.SuccessMsg(_Localizer[Result.Message], "RefreshGrid('ListAddress');Close();");
            }
            else
            {
                return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
        }

        [BindProperty]
        public viCompo_AddAddress Input { get; set; }
    }
}
