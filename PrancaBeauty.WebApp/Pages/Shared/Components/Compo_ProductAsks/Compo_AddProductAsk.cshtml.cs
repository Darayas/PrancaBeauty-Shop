using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductAsk;
using PrancaBeauty.Application.Contracts.ProductAsks;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductAsks
{
    [Authorize]
    public class Compo_AddProductAskModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductAskApplication _ProductAskApplication;

        public Compo_AddProductAskModel(ILogger logger, IServiceProvider serviceProvider, IProductAskApplication productAskApplication, IMsgBox msgBox, ILocalizer localizer, IMapper mapper)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _ProductAskApplication = productAskApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
            _Mapper = mapper;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _MappedData = _Mapper.Map<InpAddNewAsk>(Input);
                _MappedData.UserId = User.GetUserDetails().UserId;

                var _Result = await _ProductAskApplication.AddNewAskAsync(_MappedData);
                if (_Result.IsSucceeded)
                {
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "ReloadPage()");
                }
                else
                {
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);

                }
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_AddProductAsk Input { get; set; }
    }
}
