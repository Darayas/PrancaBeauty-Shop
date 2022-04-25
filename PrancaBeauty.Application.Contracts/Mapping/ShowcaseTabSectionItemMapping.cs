using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ShowcaseTabSectionItemMapping : Profile
    {
        public ShowcaseTabSectionItemMapping()
        {
            CreateMap<OutGetListShowcaseTabSectionItemForAdminPage, vmListShowcaseTabSectionItem>();
        }
    }
}
