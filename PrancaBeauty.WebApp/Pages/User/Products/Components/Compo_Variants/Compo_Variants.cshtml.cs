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
using PrancaBeauty.Application.Apps.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ProductVariantItems;
using PrancaBeauty.WebApp.Common.Types;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;

namespace PrancaBeauty.WebApp.Pages.User.Products.Components.Compo_Variants
{
    public class Compo_VariantsModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductVariantItemsApplication _ProductVariantItemsApplication;
        public Compo_VariantsModel(IMapper mapper, IProductVariantItemsApplication productVariantItemsApplication, IServiceProvider serviceProvider, IMsgBox msgBox, ILocalizer localizer)
        {
            _Mapper = mapper;
            _ProductVariantItemsApplication = productVariantItemsApplication;
            _ServiceProvider = serviceProvider;
            _MsgBox = msgBox;
            _Localizer = localizer;

            Data = new List<vmCompo_Variants>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Input.FieldName == null)
                Input.FieldName = "Input.VariantId";

            if (Input.ProductId != null)
            {
                var qData = await _ProductVariantItemsApplication.GetAllVariantsByProductIdAsync(new InpGetAllVariantsByProductId
                {
                    ProductId = Input.ProductId,
                    AuthorUserId = Input.AuthorUserId
                });

                if (qData == null)
                    return StatusCode(500);

                Data = _Mapper.Map<List<vmCompo_Variants>>(qData);

                if (qData.Count() > 0)
                    Input.VariantId = qData.First().VariantId;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveVariantsAsync(viRemoveVariants Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _Result = await _ProductVariantItemsApplication.CheckHasPurchaseForVariantAsync(new InpCheckHasPurchaseForVariant
                {
                    VariantItemId = Input.Id
                });
                if (_Result == null)
                    return _MsgBox.FaildMsg(_Localizer["Error500"]);

                if (_Result == true)
                    return _MsgBox.ModelStateMsg(_Localizer["SelectedVariantHasPurchase,CantRemove"]);
                else
                    return new JsResult($"RemoveVariants('{Input.Index}')");
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message.Replace(",", "<br/>"));
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_Variants Input { get; set; }
        public List<vmCompo_Variants> Data { get; set; }
    }
}
