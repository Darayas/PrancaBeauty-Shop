using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductPropertis;
using PrancaBeauty.Application.Contracts.ProductProperties;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.User.Products.Components.Compo_Properties
{
    public class Compo_PropertiesModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IProductPropertisApplication _ProductPropertisApplication;
        public Compo_PropertiesModel(IProductPropertisApplication productPropertisApplication, IMapper mapper)
        {
            _ProductPropertisApplication = productPropertisApplication;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync(string LangId)
        {
            if (!ModelState.IsValid)
                return StatusCode(400);

            var qData = await _ProductPropertisApplication.GetForManageProductAsync(new InpGetForManageProduct { LangId = LangId, TopicId = Input.TopicId, ProductId = Input.ProductId });
            if (qData == null)
                return StatusCode(500);

            Data = _Mapper.Map<List<vmCompo_Properties>>(qData);

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Properties Input { get; set; }
        public List<vmCompo_Properties> Data { get; set; }

    }
}
