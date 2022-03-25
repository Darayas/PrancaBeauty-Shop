using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductPrices;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductSelectedPricess
{
    public class Compo_ProductSelectedPriceModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IMapper _Mapper;
        private readonly IProductApplication _ProductApplication;
        private readonly IProductPriceApplication _ProductPriceApplication;

        public Compo_ProductSelectedPriceModel(ILogger logger, ILocalizer localizer, IProductApplication productApplication, IProductPriceApplication productPriceApplication, IServiceProvider serviceProvider, IMapper mapper)
        {
            _Logger = logger;
            _Localizer = localizer;
            _ProductApplication = productApplication;
            _ProductPriceApplication = productPriceApplication;
            _ServiceProvider = serviceProvider;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync(viGetCompo_ProductSelectedPrice Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductApplication.GetProductPriceByVariantIdAsync(new InpGetProductPriceByVariantId()
                {
                    ProductVariantItemId = Input.ProductVariantItemId
                });

                if (qData == null)
                    return StatusCode(400);

                Data = _Mapper.Map<vmCompo_ProductSelectedPrice>(qData);

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

        public vmCompo_ProductSelectedPrice Data { get; set; }
    }
}
