using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Carts;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Cart;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;

namespace PrancaBeauty.WebApp.Pages.Home
{
    public class CartModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ICartApplication _CartApplication;

        public CartModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, ICartApplication cartApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _CartApplication=cartApplication;
            _MsgBox=msgBox;
            _Localizer=localizer;
        }

        public async Task<IActionResult> OnGetAsync(string CurrencyId, string LangId)
        {
            try
            {
                #region برسی لاگین بودن کاربر
                string _UserId = null;
                {
                    if (!User.Identity.IsAuthenticated)
                        return StatusCode(401);

                    _UserId= User.GetUserDetails().UserId;
                }
                #endregion

                var qData = await _CartApplication.GetItemsInCartAsync(new InpGetItemsInCart
                {
                    LangId=LangId,
                    CurrencyId=CurrencyId,
                    UserId=_UserId
                });
                if (qData==null)
                    return StatusCode(400);

                Data= _Mapper.Map<vmCart>(qData);

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg("Error500");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region برسی لاگین بودن کاربر
                string _UserId = null;
                {
                    if (!User.Identity.IsAuthenticated)
                        return _MsgBox.InfoMsg(_Localizer["PleaseLoginToAddToCart"]);

                    _UserId= User.GetUserDetails().UserId;
                }
                #endregion

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _CartApplication.ChangeQtyCartAsync(new InpChangeQtyCart { UserId=_UserId, Items=_Mapper.Map<List<InpChangeQtyCartItem>>(Input) });
                if (_Result.IsSucceeded)
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "ReloadPage()");
                else
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg("Error500");
            }
        }

        public async Task<IActionResult> OnPostAddToCartAsync(viAddToCart Input)
        {
            try
            {
                #region برسی لاگین بودن کاربر
                string _UserId = null;
                {
                    if (!User.Identity.IsAuthenticated)
                        return _MsgBox.InfoMsg(_Localizer["PleaseLoginToAddToCart"]);

                    _UserId= User.GetUserDetails().UserId;
                }
                #endregion

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _CartApplication.AddToCartAsync(new InpAddToCart { UserId=_UserId, ProductId=Input.ProductId, SellerId=Input.SellerId, VariantItemId=Input.VariantItemId });
                if (_Result.IsSucceeded)
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message, $"/{CultureInfo.CurrentCulture.Parent.Name}/Cart"], "LoadCart()");
                else
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);

            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg("Error500");
            }
        }

        public async Task<IActionResult> OnPostRemoveAsync(viCartRemove Input)
        {
            try
            {
                #region برسی لاگین بودن کاربر
                string _UserId = null;
                {
                    if (!User.Identity.IsAuthenticated)
                        return _MsgBox.InfoMsg(_Localizer["PleaseLoginToAddToCart"]);

                    _UserId= User.GetUserDetails().UserId;
                }
                #endregion

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _CartApplication.RemoveCartItemAsync(new InpRemoveCartItem { Id=Input.Id, UserId=_UserId });
                if (_Result.IsSucceeded)
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "LoadCart()");
                else
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);

            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg("Error500");
            }
        }

        [BindProperty]
        public List<viCart> Input { get; set; }
        public vmCart Data { get; set; }
    }
}
