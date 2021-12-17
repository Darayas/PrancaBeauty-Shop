using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.User.Products.Components.Compo_Variants
{
    public class Compo_VariantsModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;
        public Compo_VariantsModel(IMapper mapper, IProductVariantItemsApplication productVariantItemsApplication)
        {
            _Mapper = mapper;
            _ProductVariantItemsApplication = productVariantItemsApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.VariantId";

            var qData = await _ProductVariantItemsApplication.GetAllVariantsByProductIdAsync(new InpGetAllVariantsByProductId
            {
                ProductId = Input.ProductId
            });

            if (qData == null)
                return StatusCode(500);

            Data = _Mapper.Map<List<vmCompo_Variants>>(qData);

            if (qData.Count() > 0)
                Input.VariantId = qData.First().VariantId;

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Variants Input { get; set; }
        public List<vmCompo_Variants> Data { get; set; }
    }
}
