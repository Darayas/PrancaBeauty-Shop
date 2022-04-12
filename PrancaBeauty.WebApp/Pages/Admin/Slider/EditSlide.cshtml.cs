using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrancaBeauty.Application.Apps.Slider;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.WebApp.Authentication;
using PrancaBeauty.WebApp.Common.Utility.MessageBox;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Admin.Slider
{
    [Authorize(Roles = Roles.CanEditSlide)]
    public class EditSlideModel : PageModel
    {
        private readonly ILogger _Logger;
        private readonly IMsgBox _MsgBox;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ILocalizer _Localizer;
        private readonly ISliderApplication _SliderApplication;

        public EditSlideModel(ILogger logger, IServiceProvider serviceProvider, ILocalizer localizer, ISliderApplication sliderApplication, IMsgBox msgBox, IMapper mapper)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _Localizer=localizer;
            _SliderApplication=sliderApplication;
            _MsgBox=msgBox;
            _Mapper=mapper;
        }

        public async Task<IActionResult> OnGetAsync(viGetEditSlide Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                ViewData["ReturnUrl"]=Input.ReturnUrl??$"/{CultureInfo.CurrentCulture.Parent.Name}/Admin/Slider/List";

                var qData = await _SliderApplication.GetSlideForEditAsync(new InpGetSlideForEdit
                {
                    Id= Input.Id
                });

                if (qData==null)
                    return StatusCode(400);

                this.Input= _Mapper.Map<viEditSlide>(qData);

                return Page();
            }
            catch (ArgumentInvalidException)
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var _MappedData = _Mapper.Map<InpSaveEditSlide>(Input);

                var _Result = await _SliderApplication.SaveEditSlideAsync(_MappedData);
                if (_Result.IsSucceeded)
                {
                    return _MsgBox.SuccessMsg(_Localizer[_Result.Message], "GotoList()");
                }
                else
                {
                    return _MsgBox.FaildMsg(_Localizer[_Result.Message]);
                }

            }
            catch (ArgumentInvalidException ex)
            {
                return _MsgBox.ModelStateMsg(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return _MsgBox.FaildMsg(_Localizer["Error500"]);
            }
        }

        [BindProperty]
        public viEditSlide Input { get; set; }
    }
}
