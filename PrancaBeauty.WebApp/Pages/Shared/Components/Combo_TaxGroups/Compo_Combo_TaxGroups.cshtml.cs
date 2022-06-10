using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.TaxGroups;
using PrancaBeauty.Application.Contracts.ApplicationDTO.TaxGroup;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Combo_TaxGroups
{
    public class Compo_Combo_TaxGroupsModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly ITaxGroupApplication _TaxGroupApplication;

        public Compo_Combo_TaxGroupsModel(IMapper mapper, ITaxGroupApplication taxGroupApplication)
        {
            _Mapper=mapper;
            _TaxGroupApplication=taxGroupApplication;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.TaxGroupId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string CountryId, string Text)
        {
            var qData = await _TaxGroupApplication.GetListTaxGroupForComboAsync(new InpGetListTaxGroupForCombo { CountryId = CountryId, Text = Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_TaxGroups>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_TaxGroups Input { get; set; }
    }
}
