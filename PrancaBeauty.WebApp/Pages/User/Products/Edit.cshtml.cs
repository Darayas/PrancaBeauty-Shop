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
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

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
        public EditModel(ILocalizer localizer, IMsgBox msgBox, IProductApplication productApplication, ILogger logger, IMapper mapper)
        {
            _Localizer = localizer;
            _MsgBox = msgBox;
            _ProductApplication = productApplication;
            _Logger = logger;
            _Mapper = mapper;
        }
        public async Task<IActionResult> OnGetAsync(string Id, string ReturnUrl = null)
        {
            try
            {
                #region Validation
                if (Id == null)
                    return StatusCode(400);

                #endregion

                ViewData["ReturnUrl"] = WebUtility.UrlDecode(ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Products/List");

                string _UserId = User.GetUserDetails().UserId;
                if (User.IsInRole(Roles.CanEditProductForAllUser))
                    _UserId = null;

                var qData = await _ProductApplication.GetForEditAsync(new Application.Contracts.Products.InpGetForEdit
                {
                    UserId = _UserId,
                    ProductId = Id
                });

                if (qData == null)
                    return StatusCode(404);

                Input = _Mapper.Map<viEditProduct>(qData);

                return Page();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        [BindProperty]
        public viEditProduct Input { get; set; }
    }
}
