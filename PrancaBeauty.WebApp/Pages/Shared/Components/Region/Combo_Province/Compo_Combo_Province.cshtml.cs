using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Province;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
using PrancaBeauty.Application.Contracts.Province;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Region.Combo_Province
{
    public class Compo_Combo_ProvinceModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IProvinceApplication _ProvinceApplication;
        public Compo_Combo_ProvinceModel(IProvinceApplication provinceApplication, IMapper mapper)
        {
            _ProvinceApplication = provinceApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.ProvinceId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string LangId, string CountryId, string Text)
        {
            var qData = await _ProvinceApplication.GetListForComboAsync(new InpGetListForCombo { LangId = LangId, CountryId = CountryId, Search = Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_Province>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Province Input { get; set; }
    }
}
