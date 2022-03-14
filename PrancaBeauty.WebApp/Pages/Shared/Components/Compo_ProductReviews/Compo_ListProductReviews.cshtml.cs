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
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
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
        private readonly IMsgBox _MsgBox;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IMapper _Mapper;
        private readonly IProductReviewsApplication _ProductReviewsApplication;
        private readonly IProductReviewsLikeApplication _ProductReviewsLikeApplication;

        public Compo_ListProductReviewsModel(ILogger logger, IServiceProvider serviceProvider, IProductReviewsApplication productReviewsApplication, IMapper mapper, IProductReviewsLikeApplication productReviewsLikeApplicatio, IMsgBox msgBox, ILocalizer localizer)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _ProductReviewsApplication = productReviewsApplication;
            _Mapper = mapper;
            _ProductReviewsLikeApplication = productReviewsLikeApplicatio;
            _MsgBox = msgBox;
            _Localizer = localizer;
        }

        public async Task<IActionResult> OnGetAsync(string LangId)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Check permissions
                bool _HasFullControl = false;
                string _UserId = null;
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        _UserId = User.GetUserDetails().UserId;

                        if (User.IsInRole(Roles.CanChangeStatusProductReviewsForAllUser))
                            _HasFullControl = true;
                    }
                }
                #endregion

                var qData = await _ProductReviewsApplication.GetReviewsForProductDetailsAsync(new InpGetReviewsForProductDetails
                {
                    LangId = LangId,
                    ProductId = Input.ProductId,
                    Take = Input.Take,
                    Page = Input.PageNum,
                    UserId = _UserId,
                    HasFullControl = _HasFullControl
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

                var Result = await _ProductReviewsLikeApplication.LikeReviewAsync(new InpLikeReview { ReviewId = Input.ReviewId, UserId = User.GetUserDetails().UserId });

                return new JsonResult(new { Count = Result.CountLike, IsLike = Result.IsLike });
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

        public async Task<IActionResult> OnPostDisLikeAsync(viCompo_ListProductReviewDisLike Input)
        {
            try
            {
                if (User.Identity.IsAuthenticated == false)
                    return new JsonResult(new { Count = -2 });

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var Result = await _ProductReviewsLikeApplication.DisLikeReviewAsync(new InpDisLikeReview { ReviewId = Input.ReviewId, UserId = User.GetUserDetails().UserId });

                return new JsonResult(new { Count = Result.CountDisLike, IsLike = Result.IsDisLike });
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

        public async Task<IActionResult> OnPostChangeStatusAsync(viCompo_ListProductReviewChangeStatus Input)
        {
            try
            {
                if (!User.IsInRole(Roles.CanChangeStatusProductReviews))
                    return _MsgBox.AccessDeniedMsg();

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                string _UserId = User.GetUserDetails().UserId;
                if (User.IsInRole(Roles.CanChangeStatusProductReviewsForAllUser))
                    _UserId = null;

                var Result = await _ProductReviewsApplication.ChanageStatusReviewAsync(new InpChanageStatusReview { ReviewId = Input.ReviewId, AuthorUserId = _UserId });

                if (Result.IsSucceeded)
                    return _MsgBox.SuccessMsg(_Localizer[Result.Message], "RefreshReviews()");
                else
                    return _MsgBox.FaildMsg(_Localizer[Result.Message]);
            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.FaildMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }


        [BindProperty(SupportsGet = true)]
        public viCompo_ListProductReviews Input { get; set; }
        public vmCompo_ListProductReviews Data { get; set; }
        public OutPagingData PagingData { get; set; }
    }
}
