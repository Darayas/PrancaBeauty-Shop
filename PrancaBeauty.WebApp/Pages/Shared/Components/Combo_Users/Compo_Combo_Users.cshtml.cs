using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Users;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Combo_Users
{
    public class Compo_Combo_UsersModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IUserApplication _UserApplication;
        public Compo_Combo_UsersModel(IMapper mapper, IUserApplication userApplication)
        {
            _Mapper = mapper;
            _UserApplication = userApplication;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.UserId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string _LangId, string Text)
        {
            var qData = await _UserApplication.GetListForComboAsync(new Application.Contracts.ApplicationDTO.Users.InpGetListForCombo { LangId = _LangId, Name = Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_Users>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Users Input { get; set; }
    }
}
