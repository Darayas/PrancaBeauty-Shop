using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class SliderMapping : Profile
    {
        public SliderMapping()
        {
            CreateMap<OutGetLstSlidesForMainSlider, vmMainPageSliderView>();
            CreateMap<OutGetListSlideForManage, vmSliderList>();

            CreateMap<viAddSlide, InpAddSlide>();
        }
    }
}
