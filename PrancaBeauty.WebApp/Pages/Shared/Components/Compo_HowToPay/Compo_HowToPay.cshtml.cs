using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.PaymentGates;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
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

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                return Page();
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

        [BindProperty(SupportsGet = true)]
        public viCompo_HowToPay Input { get; set; }
        public vmCompo_HowToPay Data{ get; set; }
    }
}
