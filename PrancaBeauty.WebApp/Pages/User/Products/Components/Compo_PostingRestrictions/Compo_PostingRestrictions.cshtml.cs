using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.PostingRestrictions;
using PrancaBeauty.Application.Contracts.ApplicationDTO.PostingRestrictions;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.WebApp.Pages.User.Products.Components.Compo_PostingRestrictions
{
    public class Compo_PostingRestrictionsModel : PageModel
    {
        private readonly IMapper _Mapper;
        private readonly IPostingRestrictionsApplication _PostingRestrictionsApplication;

        public Compo_PostingRestrictionsModel(IPostingRestrictionsApplication postingRestrictionsApplication, IMapper mapper)
        {
            _PostingRestrictionsApplication = postingRestrictionsApplication;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            if (Input.ProductId != null)
            {
                var qData = await _PostingRestrictionsApplication.GetAllPostingRestrictionsByProductIdAsync(new InpGetAllPostingRestrictionsByProductId
                {
                    ProductId = Input.ProductId
                });

                if (qData == null)
                    return StatusCode(500);

                Data = _Mapper.Map<List<vmCompo_PostingRestrictions>>(qData);
            }
            else
            {
                Data = new List<vmCompo_PostingRestrictions>();
            }

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_PostingRestrictions Input { get; set; }
        public List<vmCompo_PostingRestrictions> Data { get; set; }
    }
}
