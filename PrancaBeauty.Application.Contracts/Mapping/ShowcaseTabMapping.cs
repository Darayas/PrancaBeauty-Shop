using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ShowcaseTabMapping : Profile
    {
        public ShowcaseTabMapping()
        {
            // List
            CreateMap<OutGetListShowcaseTabForAdminPage, vmListShowcaseTabs>()
                                    .ForMember(a => a.StartDate, a => a.MapFrom(b => b.StartDate.ToString("yyyy/MM/dd HH:mm:ss")))
                                        .ForMember(a => a.EndDate, a => a.MapFrom(b => b.EndDate.HasValue ? b.EndDate.Value.ToString("yyyy/MM/dd HH:mm:ss") : ""));
        }
    }
}
