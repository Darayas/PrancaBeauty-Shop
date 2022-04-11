using AutoMapper;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PrancaBeauty.Application.Apps.Slider;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Pages.Shared.Components.MainPageSlider
{
    public class MainPageSliderViewComponent : ViewComponent
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly ISliderApplication _SliderApplication;
        public MainPageSliderViewComponent(ISliderApplication SliderApplication, ILogger Logger, IMapper mapper)
        {
            _SliderApplication= SliderApplication;
            _Logger=Logger;
            _Mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var qData = await _SliderApplication.GetLstSlidesForMainSliderAsync();
                if (qData==null)
                    return null;

                var _Data = _Mapper.Map<List<vmMainPageSliderView>>(qData);
                return View("MainPageSliderViewComponents", _Data);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }


    }
}
