using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Province;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProvinceMapping : Profile
    {
        public ProvinceMapping()
        {
            CreateMap<OutGetProvinceListForCombo, vmCompo_Combo_Province>();

        }
    }
}
