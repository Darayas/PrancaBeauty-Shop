using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductReviewsAttribute;
using PrancaBeauty.WebApp.Models.ViewInput;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductReviews
{
    public class Compo_ProductReviewAttributesModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductReviewsAttributeApplication _ProductReviewsAttributeApplication;
        public Compo_ProductReviewAttributesModel(ILogger logger, IServiceProvider serviceProvider, IProductReviewsAttributeApplication productReviewsAttributeApplication, IMapper mapper)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _ProductReviewsAttributeApplication = productReviewsAttributeApplication;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync(viGetCompo_ProductReviewAttributes Input, string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _ProductReviewsAttributeApplication.GetAttributesByTopicIdAsync(new Application.Contracts.ProductReviewsAttributes.InpGetAttributesByTopicId
                {
                    LangId = LangId,
                    TopicId = Input.TopicId
                });

                this.Input = _Mapper.Map<List<viCompo_ProductReviewAttributes>>(qData);

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

        [BindProperty(SupportsGet = true)]
        public List<viCompo_ProductReviewAttributes> Input { get; set; }
    }
}
