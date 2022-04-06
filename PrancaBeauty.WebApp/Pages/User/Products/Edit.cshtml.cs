using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Apps.Seller;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sellers;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.Products
{
    [Authorize(Roles = Roles.CanEditProduct)]
    public class EditModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly IProductApplication _ProductApplication;
        private readonly ISellerApplication _SellerApplication;
        public EditModel(ILocalizer localizer, IMsgBox msgBox, IProductApplication productApplication, ILogger logger, IMapper mapper, ISellerApplication sellerApplication)
        {
            _Localizer = localizer;
            _MsgBox = msgBox;
            _ProductApplication = productApplication;
            _Logger = logger;
            _Mapper = mapper;
            _SellerApplication = sellerApplication;
        }

        public async Task<IActionResult> OnGetAsync(string Id, string ReturnUrl = null)
        {
            try
            {
                #region Validation
                if (Id == null)
                    return StatusCode(400);

                #endregion

                ViewData["ReturnUrl"] = ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Products/List";

                string _UserId = User.GetUserDetails().UserId;
                if (User.IsInRole(Roles.CanEditProductForAllUser))
                    _UserId = null;

                var qData = await _ProductApplication.GetForEditAsync(new InpGetForEdit
                {
                    UserId = _UserId,
                    ProductId = Id
                });

                if (qData == null)
                    return StatusCode(404);

                LangId = qData.LangId;
                Input = _Mapper.Map<viEditProduct>(qData);
                ViewData["SellerId"] = await _SellerApplication.GetSellerIdByUserIdAsync(new InpGetSellerIdByUserId { UserId = Input.AuthorUserId });
                return Page();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return _MsgBox.ModelStateMsg(ModelState.GetErrors());

                var _MappedData = _Mapper.Map<InpSaveEditProduct>(Input);
                _MappedData.EditorUserId = User.GetUserDetails().UserId;
                _MappedData.CanEditThisProduct = User.IsInRole(Roles.CanEditProductForAllUser);

                var _Result = await _ProductApplication.SaveEditProductAsync(_MappedData);
                if (_Result.IsSucceeded)
                {
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "GotoBack()");
                }
                else
                {
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message.Replace(",", "<br/>")]);
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty]
        public viEditProduct Input { get; set; }
        public string LangId { get; set; }
    }
}
