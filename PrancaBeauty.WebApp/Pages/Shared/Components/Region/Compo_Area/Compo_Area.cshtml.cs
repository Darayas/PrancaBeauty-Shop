using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Region.Compo_Area
{
    public class Compo_AreaModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (Input.CountyFieldName == null)
                Input.CountyFieldName = "Input.CountryId";

            if (Input.ProvinceFieldName == null)
                Input.ProvinceFieldName = "Input.ProvinceId";

            if (Input.CityFieldName == null)
                Input.CityFieldName = "Input.CityId";

            if (Input.CountryId == null)
                Input.ProvinceId = null;

            if (Input.ProvinceId == null)
                Input.CityId = null;

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Area Input { get; set; }
    }
}
