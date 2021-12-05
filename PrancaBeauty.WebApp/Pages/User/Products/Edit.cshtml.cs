using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.WebApp.Authentication;
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
        private readonly IProductApplication _ProductApplication;
        public EditModel(ILocalizer localizer, IMsgBox msgBox, IProductApplication productApplication, ILogger logger)
        {
            _Localizer = localizer;
            _MsgBox = msgBox;
            _ProductApplication = productApplication;
            _Logger = logger;
        }
        public async Task<IActionResult> OnGetAsync(string ReturnUrl = null)
        {
            try
            {
                ViewData["ReturnUrl"] = WebUtility.UrlDecode(ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Products/List");

                return Page();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty]
        public viEditProduct Input { get; set; }
    }
}
