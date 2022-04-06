using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Seller;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sellers;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_Sellers
{
    public class Compo_Combo_SellersModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly ISellerApplication _SellerApplication;

        public Compo_Combo_SellersModel(IMapper mapper, ISellerApplication sellerApplication)
        {
            _Mapper = mapper;
            _SellerApplication = sellerApplication;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.SellerId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string _LangId, string Text)
        {
            var qData = await _SellerApplication.GetListSellerForComboAsync(new InpGetListSellerForCombo { LangId = _LangId, SellerTitle = Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_Sellers>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Sellers Input { get; set; }
    }
}
