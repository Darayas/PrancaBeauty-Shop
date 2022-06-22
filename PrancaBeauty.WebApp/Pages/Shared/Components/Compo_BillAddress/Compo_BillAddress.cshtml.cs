using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Address;
using PrancaBeauty.Application.Apps.Bills;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Address;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Bills;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_BillAddress
{
    [Authorize]
    public class Compo_BillAddressModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IAddressApplication _AddressApplication;
        private readonly IBillApplication _BillApplication;

        public Compo_BillAddressModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IAddressApplication addressApplication, IBillApplication billApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _AddressApplication=addressApplication;
            _BillApplication=billApplication;
            _MsgBox=msgBox;
            _Localizer=localizer;
        }

        public async Task<IActionResult> OnGetAsync(string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _AddressApplication.GetListAddressForBillsAsync(new InpGetListAddressForBills
                {
                    LangId=LangId,
                    UserId=Input.BuyerUserId,
                    IsBuyer=Input.IsBuyer,
                    AddressId=Input.AddressId
                });
                if (qData==null)
                    return StatusCode(500);

                Data= _Mapper.Map<List<vmCompo_BillAddress>>(qData);

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

        public async Task<IActionResult> OnPostUpdateBillAddressAsync(viUpdateBillAddress Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                string _UserId = User.GetUserDetails().UserId;
                var _Result = await _BillApplication.ChangeBillAddressAsync(new InpChangeBillAddress { BillId=Input.BillId, BuyerUserId= _UserId, AddressId=Input.AddressId });
                if (!_Result.IsSucceeded)
                    return _MsgBox.ModelStateMsg(_Result.Message);

                return new JsResult("ReloadPage()");
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.ModelStateMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_BillAddress Input { get; set; }
        public List<vmCompo_BillAddress> Data { get; set; }
    }
}
