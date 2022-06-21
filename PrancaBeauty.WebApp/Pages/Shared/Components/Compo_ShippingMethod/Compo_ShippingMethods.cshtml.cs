using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ShippingMethods;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShippingMethods;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ShippingMethod
{
    [Authorize]
    public class Compo_ShippingMethodsModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShippingMethodApplication _ShippingMethodApplication;

        public Compo_ShippingMethodsModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IShippingMethodApplication shippingMethodApplication)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _ShippingMethodApplication=shippingMethodApplication;
        }

        public async Task<IActionResult> OnGetAsync(string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ShippingMethodApplication.GetShippingMethodForBillAsync(new InpGetShippingMethodForBill
                {
                    LangId=LangId,
                    BuyerAddressId=Input.BuyerAddressId,
                    SellerAddressId=Input.SellerAddressId
                });
                if (qData==null)
                    return StatusCode(400);

                Data= _Mapper.Map<List<vmCompo_ShippingMethods>>(qData);

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
        public viCompo_ShippingMethods Input { get; set; }
        public List<vmCompo_ShippingMethods> Data { get; set; }
    }
}
