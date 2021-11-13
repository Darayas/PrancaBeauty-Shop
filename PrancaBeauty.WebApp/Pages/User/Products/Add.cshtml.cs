using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.User.Products
{
    [Authorize(Roles = Roles.CanAddProduct)]
    public class AddModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IProductApplication _ProductApplication;

        public AddModel(IMapper mapper, ILogger logger, IProductApplication productApplication, IMsgBox msgBox, ILocalizer localizer)
        {
            _Mapper = mapper;
            _Logger = logger;
            _ProductApplication = productApplication;
            _MsgBox = msgBox;
            _Localizer = localizer;
        }

        public IActionResult OnGet(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = WebUtility.UrlDecode(ReturnUrl ?? $"/{CultureInfo.CurrentCulture.Parent.Name}/User/Products/List");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validation
                if (!ModelState.IsValid)
                    return _MsgBox.ModelStateMsg(ModelState.GetErrors());

                if (Input.Properties is null)
                    throw new ArgumentInvalidException($"Properties cant be null.");

                if (Input.Properties.Any(a => string.IsNullOrWhiteSpace(a.Value)))
                    throw new ArgumentInvalidException($"Properties value cant be null or whitespace.");

                if (Input.Keywords is null)
                    throw new ArgumentInvalidException($"Keywords cant be null.");

                if (Input.Keywords.Count() == 0)
                    throw new ArgumentInvalidException($"keyword count must be greater than zero.");
                #endregion

                var _Result = await _ProductApplication.AddProdcutAsync(_Mapper.Map<InpAddProdcut>(Input), "");
                if (_Result.IsSucceeded)
                {

                }
                else
                {

                }

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(_Localizer[ex.Message]);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty]
        public viAddProduct Input { get; set; }
    }
}
