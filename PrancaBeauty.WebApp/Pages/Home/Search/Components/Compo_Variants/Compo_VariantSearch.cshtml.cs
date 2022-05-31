using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductVariants;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariants;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Home.Search.Components.Compo_Variants
{
    public class Compo_VariantSearchModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductVariantsApplication _ProductVariantsApplication;

        public Compo_VariantSearchModel(ILogger logger, IServiceProvider serviceProvider, IProductVariantsApplication productVariantsApplication, IMapper mapper)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ProductVariantsApplication=productVariantsApplication;
            _Mapper=mapper;
        }

        public async Task<IActionResult> OnGetAsync(string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductVariantsApplication.GetVariantsForSearchByCateIdAsync(new InpGetVariantsForSearchByCateId { LangId=LangId, CategoryId=Input.CategoryId });
                Data= _Mapper.Map<List<vmCompo_VariantSearch>>(qData);

                return Page();
            }
            catch (ArgumentInvalidException)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_VariantSearch Input { get; set; }
        public List<vmCompo_VariantSearch> Data { get; set; }
    }
}
