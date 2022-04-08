using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Sellers;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class SellerMapping : Profile
    {
        public SellerMapping()
        {
            CreateMap<OutGetSummaryBySellerId, vmProductSellerDetails>().ForMember(x => x.DateTime, opt => opt.MapFrom(src => src.DateTime.ToString("yyyy-MM-dd")));
            CreateMap<OutGetListSellerForCombo, vmCompo_Combo_Sellers>();
        }
    }
}
