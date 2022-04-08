using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.City;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class CityMapping : Profile
    {
        public CityMapping()
        {
            CreateMap<OutGetCityListForCombo, vmCompo_Combo_Cities>();

        }
    }
}
