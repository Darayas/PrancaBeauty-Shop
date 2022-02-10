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
using PrancaBeauty.Application.Apps.Products;
using PrancaBeauty.Application.Contracts.Products;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.Home
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductApplication _ProductApplication;
        public ProductDetailsModel(IServiceProvider serviceProvider, ILogger logger, IProductApplication productApplication, IMapper mapper)
        {
            _ServiceProvider = serviceProvider;
            _Logger = logger;
            _ProductApplication = productApplication;
            _Mapper = mapper;
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

                // Name is invalid
                if (qData.IsConfirm == false && qData.Product == null)
                    return StatusCode(404);

                if (qData.IsConfirm == false && qData.Product != null)
                {
                    return StatusCode(410);
                }

                // TODO اگر حذف شده بود کد وضعیت مناسب برگشت داده شود
                // TODO اگر پیش نویس بود کد وضعیت مناسب برگشت داده شود

                Data = _Mapper.Map<vmProductDetails>(qData.Product);

                return Page();
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

        public vmProductDetails Data { get; set; }
    }
}
