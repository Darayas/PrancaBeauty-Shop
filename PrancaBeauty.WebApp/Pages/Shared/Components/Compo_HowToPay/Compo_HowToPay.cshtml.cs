using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.PaymentGates;
using PrancaBeauty.Application.Contracts.ApplicationDTO.PaymentGate;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_HowToPay
{
    [Authorize]
    public class Compo_HowToPayModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IPaymentGateApplication _PaymentGateApplication;

        public Compo_HowToPayModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IPaymentGateApplication paymentGateApplication)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _PaymentGateApplication=paymentGateApplication;
        }

        public async Task<IActionResult> OnGetAsync(string LangId, string CountryId, string CurrencyId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _PaymentGateApplication.GetPaymentGateByCountryAsync(new InpGetPaymentGateByCountry { LangId=LangId, CountryId=CountryId, CurrencyId=CurrencyId });
                if (qData == null)
                    return StatusCode(500);

                Data= _Mapper.Map<List<vmCompo_HowToPay>>(qData);

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
        public viCompo_HowToPay Input { get; set; }
        public List<vmCompo_HowToPay> Data { get; set; }
    }
}
