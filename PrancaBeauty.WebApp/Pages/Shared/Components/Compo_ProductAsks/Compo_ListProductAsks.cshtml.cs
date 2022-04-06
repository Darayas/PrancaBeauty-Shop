using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductAsk;
using PrancaBeauty.Application.Apps.ProductAskLikes;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProdcutReviews;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductAskLikes;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductAsks;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductReviewLikes;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductAsks
{
    public class Compo_ListProductAsksModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IMsgBox _MsgBox;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IProductAskApplication _ProductAskApplication;
        private readonly IProductAskLikesApplication _ProductAskLikesApplication;

        public Compo_ListProductAsksModel(ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer, IProductAskApplication productAskApplication, IMapper mapper, IProductAskLikesApplication productAskLikesApplication, IMsgBox msgBox)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _Localizer = localizer;
            _ProductAskApplication = productAskApplication;
            _Mapper = mapper;
            _ProductAskLikesApplication = productAskLikesApplication;
            _MsgBox = msgBox;
        }

        public async Task<IActionResult> OnGetAsync()
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

                        if (User.IsInRole(Roles.CanChangeStatusProductAsksForAllUser))
                            _HasFullControl = true;
                    }
                }
                #endregion

                var qData = await _ProductAskApplication.GetListAsksAsync(new InpGetListAsks
                {
                    ProductId = Input.ProductId,
                    Take = Input.Take,
                    Page = Input.PageNum,
                    UserId = _UserId,
                    HasFullControl = _HasFullControl
                });

                PagingData = qData.PagingData;
                Data = _Mapper.Map<List<vmCompo_ListProductAsks>>(qData.LstAsks);

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

        public async Task<IActionResult> OnPostLikeAsync(viCompo_ListProductAskLike Input)
        {
            try
            {
                if (User.Identity.IsAuthenticated == false)
                    return new JsonResult(new { Count = -2 });

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var Result = await _ProductAskLikesApplication.LikeAskAsync(new InpLikeAsk { AskId = Input.AskId, UserId = User.GetUserDetails().UserId });

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

        public async Task<IActionResult> OnPostDisLikeAsync(viCompo_ListProductAskDisLike Input)
        {
            try
            {
                if (User.Identity.IsAuthenticated == false)
                    return new JsonResult(new { Count = -2 });

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var Result = await _ProductAskLikesApplication.DisLikeAskAsync(new InpDisLikeAsk { AskId = Input.AskId, UserId = User.GetUserDetails().UserId });

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

        public async Task<IActionResult> OnPostChangeStatusAsync(viCompo_ListProductAskChangeStatus Input)
        {
            try
            {
                if (!User.IsInRole(Roles.CanChangeStatusProductAsks))
                    return _MsgBox.AccessDeniedMsg();

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                string _UserId = User.GetUserDetails().UserId;
                if (User.IsInRole(Roles.CanChangeStatusProductAsksForAllUser))
                    _UserId = null;

                var Result = await _ProductAskApplication.ChanageStatusAskAsync(new InpChanageStatusAsk { AskId = Input.AskId, AuthorUserId = _UserId });

                if (Result.IsSucceeded)
                    return _MsgBox.SuccessMsg(_Localizer[Result.Message], "RefreshAsks()");
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

        public async Task<IActionResult> OnPostRemoveAsync(viCompo_ListProductAskRemove Input)
        {
            try
            {
                if (!User.IsInRole(Roles.CanRemoveProductAsks))
                    return _MsgBox.AccessDeniedMsg();

                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                string _UserId = User.GetUserDetails().UserId;
                if (User.IsInRole(Roles.CanRemoveProductAsksForAllUser))
                    _UserId = null;

                var _Result = await _ProductAskApplication.RemoveAskAsync(new InpRemoveAsk { AskId = Input.AskId, UserId = _UserId });

                if (_Result.IsSucceeded)
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "RefreshAsks()");
                else
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
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
        public viCompo_ListProductAsks Input { get; set; }

        public List<vmCompo_ListProductAsks> Data { get; set; }
        public OutPagingData PagingData { get; set; }
    }
}
