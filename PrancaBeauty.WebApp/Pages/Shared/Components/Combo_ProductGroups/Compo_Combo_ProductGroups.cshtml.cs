using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductGroup;
using PrancaBeauty.Application.Apps.TaxGroups;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductGroups;
using PrancaBeauty.Application.Contracts.ApplicationDTO.TaxGroup;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Combo_ProductGroups
{
    public class Compo_Combo_ProductGroupsModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IProductGroupApplication _ProductGroupApplication;

        public Compo_Combo_ProductGroupsModel(IMapper mapper, IProductGroupApplication productGroupApplication)
        {
            _Mapper=mapper;
            _ProductGroupApplication=productGroupApplication;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.ProductGroupId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string LangId, string Text)
        {
            var qData = await _ProductGroupApplication.GetListProductGroupForComboAsync(new InpGetListProductGroupForCombo { TopicId = Input.TopicId,LangId=LangId, Text = Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_TaxGroups>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_ProductGroups Input { get; set; }
    }
}
