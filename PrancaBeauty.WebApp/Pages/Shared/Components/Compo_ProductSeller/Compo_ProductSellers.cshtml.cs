using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductSellers;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductSeller
{
    public class Compo_ProductSellersModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductSellersApplication _ProductSellersApplication;

        public Compo_ProductSellersModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IProductSellersApplication productSellersApplication)
        {
            _Logger = logger;
            _Mapper = mapper;
            _ServiceProvider = serviceProvider;
            _ProductSellersApplication = productSellersApplication;
        }

        public async Task<IActionResult> OnGetAsync(viGetCompo_ProductSellers Input)
        {
            try
            {
                #region Validations
                //Input.CheckModelState(_ServiceProvider);
                #endregion

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        public List<vmCompo_ProductSellers> Data { get; set; }
    }
}
