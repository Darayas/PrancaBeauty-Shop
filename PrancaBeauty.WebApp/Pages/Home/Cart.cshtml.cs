using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Carts;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;

namespace PrancaBeauty.WebApp.Pages.Home
{
    public class CartModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ICartApplication _CartApplication;

        public CartModel(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, ICartApplication cartApplication, IMsgBox msgBox)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _CartApplication=cartApplication;
            _MsgBox=msgBox;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(viAddToCart Input)
        {
            try
            {
                #region برسی لاگین بودن کاربر
                string _UserId = null;
                {
                    if (!User.Identity.IsAuthenticated)
                        return _MsgBox.InfoMsg("PleaseLoginToAddToCart");

                    _UserId= User.GetUserDetails().UserId;
                }
                #endregion

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion


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
    }
}
