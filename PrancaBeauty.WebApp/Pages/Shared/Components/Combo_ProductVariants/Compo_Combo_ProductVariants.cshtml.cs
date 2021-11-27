using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductVariants;
using PrancaBeauty.Application.Contracts.ProductVariants;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Combo_ProductVariants
{
    public class Compo_Combo_ProductVariantsModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IProductVariantsApplication _ProductVariantsApplication;

        public Compo_Combo_ProductVariantsModel(IProductVariantsApplication productVariantsApplication, IMapper mapper)
        {
            _ProductVariantsApplication = productVariantsApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.VariantId";

            return Page();
        }

        public async Task<JsonResult> OnGetReadAsync(string LangId, string Text)
        {
            var qData = await _ProductVariantsApplication.GetLstForComboAsync(new InpGetLstForCombo { LangId = LangId, Text = Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_ProductVariants>>(qData);

            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_ProductVariants Input { get; set; }
    }
}
