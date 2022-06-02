using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Carts;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Cart;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Common.ExMethod;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_CartWidget
{
    [Authorize]
    public class Compo_CartWidgetMainModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ICartApplication _CartApplication;

        public Compo_CartWidgetMainModel(ILogger logger, IServiceProvider serviceProvider, ICartApplication cartApplication, IMapper mapper)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _CartApplication=cartApplication;
            _Mapper=mapper;
        }

        public async Task<IActionResult> OnGetAsync(string LangId, string CurrencyId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                string _UserId = User.GetUserDetails().UserId;

                var qData = await _CartApplication.GetItemsInCartAsync(new InpGetItemsInCart { CurrencyId=CurrencyId, UserId=_UserId, LangId=LangId });
                if (qData==null)
                {
                    Data= null;
                    return Page();
                }

                Data= _Mapper.Map<vmCompo_CartWidgetMain>(qData);

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
        public viCompo_CartWidgetMain Input { get; set; }
        public vmCompo_CartWidgetMain Data { get; set; }
    }
}
