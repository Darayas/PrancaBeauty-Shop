using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Countries;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class CountriesMapping : Profile
    {
        public CountriesMapping()
        {
            CreateMap<OutGetCourtriesListForCombo, vmCompo_Combo_Countries>();
        }
    }
}
