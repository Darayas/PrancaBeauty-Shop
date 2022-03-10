using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using PrancaBeauty.Application.Apps.ProductReviews;
using PrancaBeauty.Application.Contracts.ProdcutReviews;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductReviews
{
    public class Compo_ListProductReviewsModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IMapper _Mapper;
        private readonly IProductReviewsApplication _ProductReviewsApplication;

        public Compo_ListProductReviewsModel(ILogger logger, IServiceProvider serviceProvider, IProductReviewsApplication productReviewsApplication, IMapper mapper)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _ProductReviewsApplication = productReviewsApplication;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync(string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                Input.Take = 1;
                var qData = await _ProductReviewsApplication.GetReviewsForProductDetailsAsync(new InpGetReviewsForProductDetails
                {
                    LangId = LangId,
                    ProductId = Input.ProductId,
                    Take = Input.Take,
                    Page = Input.PageNum
                });

                Data = _Mapper.Map<vmCompo_ListProductReviews>(qData.RevivewsData);
                PagingData = qData.PageingData;

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
        public viCompo_ListProductReviews Input { get; set; }
        public vmCompo_ListProductReviews Data { get; set; }
        public OutPagingData PagingData { get; set; }
    }
}
