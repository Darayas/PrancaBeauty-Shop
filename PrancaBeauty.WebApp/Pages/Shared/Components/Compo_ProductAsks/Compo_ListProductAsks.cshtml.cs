using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductAsk;
using PrancaBeauty.Application.Apps.ProductAskLikes;
using PrancaBeauty.Application.Contracts.ProductAskLikes;
using PrancaBeauty.Application.Contracts.ProductAsks;
using PrancaBeauty.Application.Contracts.ProductReviewLikes;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Models.ViewInput;
using PrancaBeauty.WebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductAsks
{
    public class Compo_ListProductAsksModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly IProductAskApplication _ProductAskApplication;
        private readonly IProductAskLikesApplication _ProductAskLikesApplication;

        public Compo_ListProductAsksModel(ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer, IProductAskApplication productAskApplication, IMapper mapper)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _Localizer = localizer;
            _ProductAskApplication = productAskApplication;
            _Mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                Input.Take = 1;

                var qData = await _ProductAskApplication.GetListAsksAsync(new InpGetListAsks { ProductId = Input.ProductId, Take = Input.Take, Page = Input.PageNum });

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

        [BindProperty(SupportsGet = true)]
        public viCompo_ListProductAsks Input { get; set; }

        public List<vmCompo_ListProductAsks> Data { get; set; }
        public OutPagingData PagingData { get; set; }
    }
}
