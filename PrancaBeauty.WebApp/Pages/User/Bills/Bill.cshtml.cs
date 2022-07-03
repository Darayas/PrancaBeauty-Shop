using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Bills;
using PrancaBeauty.Application.Apps.Settings;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Bills;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Settings;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.User.Bills
{
    [Authorize]
    public class BillModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IBillApplication _BillApplication;
        private readonly ISettingApplication _SettingApplication;

        public BillModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, IBillApplication billApplication, ISettingApplication settingApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _BillApplication=billApplication;
            _SettingApplication=settingApplication;
            _MsgBox=msgBox;
            _Localizer=localizer;
        }

        public async Task<IActionResult> OnGetAsync(string CurrencyId, string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Check Access
                string _BuyerUserId = null;
                string _SellerUserId = null;
                {
                    if (User.IsInRole(Roles.CanViewListBillAdmin))
                    {
                        _BuyerUserId=null;
                        _SellerUserId=null;
                    }
                    else if (User.IsInRole(Roles.CanViewListBillSeller))
                    {
                        _SellerUserId = User.GetUserDetails().SellerId;
                    }
                    else
                    {
                        _SellerUserId=null;
                        _BuyerUserId = User.GetUserDetails().UserId;
                    }
                }
                #endregion

                var qData = await _BillApplication.GetBillDetailsAsync(new InpGetBillDetails { BillNumber=Input.BillNumber, CurrencyId=CurrencyId, LangId=LangId, BuyerUserId=_BuyerUserId, SellerUserId=_SellerUserId });
                Data=_Mapper.Map<vmBill>(qData);

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

        public async Task<IActionResult> OnPostPaymentAsync(viBillPayment Input, string CurrencyId)
        {
            try
            {
                var req = Request;
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Get site setting
                var _Setting = await _SettingApplication.GetSettingAsync(new InpGetSetting { LangCode= CultureInfo.CurrentCulture.Name });
                #endregion

                var _Result = await _BillApplication.StartPaymentAsync(new InpStartPayment
                {
                    BillNumber=Input.BillNumber,
                    CurrencyId=CurrencyId,
                    CallBackUrl=$"{_Setting.SiteUrl}/User/Bill/{Input.BillNumber}?handler=PaymentVeryfication",
                    UserId=User.GetUserDetails().UserId
                });
                if (_Result.IsSucceeded)
                    return new JsResult($"location.href='{_Result.Data.PaymentyGateUrl}'");
                else
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.FaildMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }

        public async Task<IActionResult> OnPostPaymentVeryficationAsync()
        {
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public viBill Input { get; set; }
        public vmBill Data { get; set; }
    }
}
