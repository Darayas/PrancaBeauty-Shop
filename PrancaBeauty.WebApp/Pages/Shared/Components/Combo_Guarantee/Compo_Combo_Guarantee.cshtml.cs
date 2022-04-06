using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Guarantee;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Guarantee;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Combo_Guarantee
{
    public class Compo_Combo_GuaranteeModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IGuaranteeApplications _GuaranteeApplications;

        public Compo_Combo_GuaranteeModel(IMapper mapper, IGuaranteeApplications guaranteeApplications)
        {
            _Mapper = mapper;
            _GuaranteeApplications = guaranteeApplications;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string LangId, string Text)
        {
            var qData = await _GuaranteeApplications.GetListForComboAsync(new InpGetListForCombo { LangId = LangId, Search = Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_Guarantee>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Guarantee Input { get; set; }
    }
}
