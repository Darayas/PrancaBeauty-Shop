using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using PrancaBeauty.Application.Apps.ProductReviews;
using PrancaBeauty.Application.Apps.ProductReviewsLike;
using PrancaBeauty.Application.Contracts.ProdcutReviews;
using PrancaBeauty.Application.Contracts.ProductReviewLikes;
using PrancaBeauty.WebApp.Common.ExMethod;
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
        private readonly IProductReviewsLikeApplication _ProductReviewsLikeApplicatio;

        public Compo_ListProductReviewsModel(ILogger logger, IServiceProvider serviceProvider, IProductReviewsApplication productReviewsApplication, IMapper mapper, IProductReviewsLikeApplication productReviewsLikeApplicatio)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _ProductReviewsApplication = productReviewsApplication;
            _Mapper = mapper;
            _ProductReviewsLikeApplicatio = productReviewsLikeApplicatio;
        }

        public async Task<IActionResult> OnGetAsync(string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                string _UserId = null;
                if (User.Identity.IsAuthenticated)
                    _UserId = User.GetUserDetails().UserId;

                Input.Take = 1;
                var qData = await _ProductReviewsApplication.GetReviewsForProductDetailsAsync(new InpGetReviewsForProductDetails
                {
                    LangId = LangId,
                    ProductId = Input.ProductId,
                    Take = Input.Take,
                    Page = Input.PageNum,
                    UserId = _UserId
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

        public async Task<IActionResult> OnPostLikeAsync(viCompo_ListProductReviewLike Input)
        {
            try
            {
                if (User.Identity.IsAuthenticated == false)
                    return new JsonResult(new { Count = -2 });

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var Result = await _ProductReviewsLikeApplicatio.LikeReviewAsync(new InpLikeReview { ReviewId = Input.ReviewId, UserId = User.GetUserDetails().UserId });

                return new JsonResult(new { Count = Result });
            }
            catch (ArgumentInvalidException)
            {
                return new JsonResult(new { Count = -1 });
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new JsonResult(new { Count = -1 });
            }
        }

        [BindProperty(SupportsGet = true)]
        public viCompo_ListProductReviews Input { get; set; }
        public vmCompo_ListProductReviews Data { get; set; }
        public OutPagingData PagingData { get; set; }
    }
}
