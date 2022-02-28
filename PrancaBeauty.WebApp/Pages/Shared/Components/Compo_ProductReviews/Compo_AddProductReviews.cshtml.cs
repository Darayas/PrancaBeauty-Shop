using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.ProductReviews;
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
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IProductReviewsApplication _ProductReviewsApplication;

        public Compo_AddProductReviewsModel(ILogger logger, IServiceProvider serviceProvider, IMsgBox msgBox, ILocalizer localizer, IProductReviewsApplication productReviewsApplication)
        {
            _Logger = logger;
            _ServiceProvider = serviceProvider;
            _MsgBox = msgBox;

            Input = new viCompo_AddProductReviews();
            _Localizer = localizer;
            _ProductReviewsApplication = productReviewsApplication;
        }

        public IActionResult OnGet(viGetCompo_AddProductReviews Input)
        {
            this.Input.TopicId = Input.TopicId;
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

                return _MsgBox.SuccessMsg("");
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
