using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductDrp
{
    public class Compo_Combo_ProductsModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IProductApplication _ProductApplication;

        public Compo_Combo_ProductsModel(IMapper mapper, IProductApplication productApplication)
        {
            _Mapper=mapper;
            _ProductApplication=productApplication;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.ProductId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string Text)
        {
            var qData = await _ProductApplication.GetProductListForComboAsync(new InpGetProductListForCombo { Title=Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_Products>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Products Input { get; set; }
    }
}
