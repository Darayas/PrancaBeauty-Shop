using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Cities;
using PrancaBeauty.Application.Contracts.ApplicationDTO.City;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Region.Combo_City
{
    public class Compo_Combo_CitiesModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly ICityApplication _CityApplication;
        public Compo_Combo_CitiesModel(ICityApplication cityApplication, IMapper mapper)
        {
            _CityApplication = cityApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.ProvinceId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string LangId, string ProvinceId, string Text)
        {
            var qData = await _CityApplication.GetListForComboAsync(new InpGetListForCombo { LangId = LangId, ProvinceId = ProvinceId, Search = Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_Cities>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Cities Input { get; set; }
    }
}
