using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Models.ViewInput;

namespace PrancaBeauty.WebApp.Pages.Home
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductApplication _ProductApplication;
        public ProductDetailsModel(IServiceProvider serviceProvider, ILogger logger, IProductApplication productApplication)
        {
            _ServiceProvider = serviceProvider;
            _Logger = logger;
            _ProductApplication = productApplication;
        }
        public async Task<IActionResult> OnGetAsync(viGetProductDetails Input, string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                bool _CheckConfirm = true;
                if (User.IsInRole(Roles.CanChangeStatusProduct))
                    _CheckConfirm = false;

                var qData = await _ProductApplication.GetProductForDetailsAsync(new InpGetProductForDetails
                {
                    LangId = LangId,
                    Name = Input.Name,
                    CheckConfirm = _CheckConfirm
                });


            }
            catch (ArgumentInvalidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }


    }
}
