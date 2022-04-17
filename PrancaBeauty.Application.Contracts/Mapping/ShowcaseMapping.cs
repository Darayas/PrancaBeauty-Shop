using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Globalization;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ShowcaseMapping : Profile
    {
        public ShowcaseMapping()
        {
            // Add
            CreateMap<viAddShowcase, InpAddShowcase>();
            CreateMap<viAddShowcase_Translate, InpAddShowcase_Translate>();
            
            // Edit
            CreateMap<OutGetShowcaseForEdit, viEditShowcase>()
                                .ForMember(x => x.StartDate,opt => opt.MapFrom(src => src.StartDate.HasValue? src.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")):""))
                                .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate.HasValue? src.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")):""));
            CreateMap<OutGetShowcaseForEdit_Translate, viEditShowcase_Translate>();
            CreateMap<viEditShowcase, InpSaveEditShowcase>();
            CreateMap<viEditShowcase_Translate, InpSaveEditShowcase_Translate>();

            // List
            CreateMap<OutGetListShowcaseForAdminPage, vmListShowcases>()
                                        .ForMember(a => a.StartDate, a => a.MapFrom(b =>  b.StartDate.ToString("yyyy/MM/dd HH:mm:ss")))
                                        .ForMember(a => a.EndDate, a => a.MapFrom(b => b.EndDate.HasValue ? b.EndDate.Value.ToString("yyyy/MM/dd HH:mm:ss") : ""));
        }
    }
}
