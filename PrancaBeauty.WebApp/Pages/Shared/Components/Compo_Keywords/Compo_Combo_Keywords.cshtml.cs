using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Keywords;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Keywords;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_Keywords
{
    public class Compo_Combo_KeywordsModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IKeywordApplication _KeywordApplication;

        public Compo_Combo_KeywordsModel(IMapper mapper, IKeywordApplication keywordApplication)
        {
            _Mapper=mapper;
            _KeywordApplication=keywordApplication;
        }

        public IActionResult OnGet()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.KeywordId";

            return Page();
        }

        public async Task<IActionResult> OnGetReadAsync(string Text)
        {
            var qData = await _KeywordApplication.GetKeywordListForComboAsync(new InpGetKeywordListForCombo { Title=Text });
            var Data = _Mapper.Map<List<vmCompo_Combo_Keywords>>(qData);
            return new JsonResult(Data);
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Combo_Keywords Input { get; set; }
    }
}
