using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductPrices;
using PrancaBeauty.Application.Apps.Products;
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
        private readonly IProductApplication _ProductApplication;
        private readonly IProductPriceApplication _ProductPriceApplication;

        public Compo_ProductSelectedPriceModel(ILogger logger, ILocalizer localizer, IProductApplication productApplication, IProductPriceApplication productPriceApplication, IServiceProvider serviceProvider)
        {
            _Logger = logger;
            _Localizer = localizer;
            _ProductApplication = productApplication;
            _ProductPriceApplication = productPriceApplication;
            _ServiceProvider = serviceProvider;
        }

        public async Task<IActionResult> OnGetAsync(viGetCompo_ProductSelectedPrice Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion


            }
            catch (ArgumentInvalidException ex)
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
