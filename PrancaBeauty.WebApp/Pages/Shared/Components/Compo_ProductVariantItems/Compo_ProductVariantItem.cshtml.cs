using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductVariantItems
{
    public class Compo_ProductVariantItemModel : PageModel
    {

        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;

        public Compo_ProductVariantItemModel(ILogger logger, IMapper mapper, IProductVariantItemsApplication ProductVariantItemsApplication, IServiceProvider serviceProvider)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ProductVariantItemsApplication=ProductVariantItemsApplication;
            _ServiceProvider=serviceProvider;
        }



        public async Task<IActionResult> OnGetAsync(viCompo_ProductVariantItem Input, string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductVariantItemsApplication.GetAllProductVariantsForProductDetailsAsync(new InpGetAllProductVariantsForProductDetails
                {
                    LangId=LangId,
                    ProductId=Input.ProductId
                });

                Data= _Mapper.Map<vmCompo_ProductVariantItem>(qData);
                Data.DefaultVariantValue=Input.ProductVariantValue;

                return Page();
            }
            catch (ArgumentInvalidException ex)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        public vmCompo_ProductVariantItem Data { get; set; }
    }
}
