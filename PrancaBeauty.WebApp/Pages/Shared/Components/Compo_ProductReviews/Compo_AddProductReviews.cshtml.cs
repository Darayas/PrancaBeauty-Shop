using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductReviews;
using PrancaBeauty.Application.Contracts.ProdcutReviews;
using PrancaBeauty.WebApp.Common.ExMethod;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using PrancaBeauty.WebApp.Models.ViewInput;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.Compo_ProductReviews
{
    [Authorize]
    public class Compo_AddProductReviewsModel : PageModel
    {
        private readonly IMsgBox _MsgBox;
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductReviewsApplication _ProductReviewsApplication;

        public Compo_AddProductReviewsModel(ILogger logger, IServiceProvider serviceProvider, IMsgBox msgBox, ILocalizer localizer, IProductReviewsApplication productReviewsApplication, IMapper mapper)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _MsgBox = msgBox;

            Input = new viCompo_AddProductReviews();
            _Localizer = localizer;
            _ProductReviewsApplication = productReviewsApplication;
            _Mapper = mapper;
        }

        public IActionResult OnGet(viGetCompo_AddProductReviews Input)
        {
            ViewData["TopicId"] = Input.TopicId;
            this.Input.ProductId = Input.ProductId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _MappedData = _Mapper.Map<InpAddReviewFromUser>(Input);
                _MappedData.AuthorUserId = User.GetUserDetails().UserId;
                _MappedData.IpAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                var _Result = await _ProductReviewsApplication.AddReviewFromUserAsync(_MappedData);
                if (_Result.IsSucceeded)
                {
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message]);
                }
                else
                {
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
                }
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

        [BindProperty]
        public viCompo_AddProductReviews Input { get; set; }

    }
}
