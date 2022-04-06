using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Keywords;
using PrancaBeauty.Application.Apps.KeywordsProducts;
using PrancaBeauty.Application.Contracts.ApplicationDTO.KeywordProducts;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.WebApp.Pages.User.Products.Components.Compo_Keywords
{
    public class Compo_Input_KeywordsModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IKeywordProductsApplication _KeywordProductsApplication;

        public Compo_Input_KeywordsModel(IKeywordProductsApplication keywordProductsApplication, IMapper mapper)
        {
            _KeywordProductsApplication = keywordProductsApplication;

            Data = new List<vmCompo_Input_Keywords>();
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Input.ProductId != null)
            {
                var qData = await _KeywordProductsApplication.GetKeywordByProductIdAsync(new InpGetKeywordByProductId { ProductId = Input.ProductId });
                if (qData == null)
                    Data = new List<vmCompo_Input_Keywords>();

                Data = _Mapper.Map<List<vmCompo_Input_Keywords>>(qData);
            }

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Input_Keywords Input { get; set; }
        public List<vmCompo_Input_Keywords> Data { get; set; }
    }
}
