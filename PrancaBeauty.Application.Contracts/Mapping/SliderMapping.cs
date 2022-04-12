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

            CreateMap<viEditSlide, InpSaveEditSlide>();
            CreateMap<OutGetSlideForEdit, viEditSlide>()
                                        .ForMember(a=>a.StartDate,a=> a.MapFrom(b=>b.StartDate.HasValue?b.StartDate.Value.ToString("yyyy/MM/dd HH:mm:ss"):""))
                                        .ForMember(a=>a.EndDate,a=> a.MapFrom(b=>b.EndDate.HasValue?b.EndDate.Value.ToString("yyyy/MM/dd HH:mm:ss"):""));
        }
    }
}
