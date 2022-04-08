using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Currency;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class CurrencyMapping : Profile
    {
        public CurrencyMapping()
        {
            CreateMap<OutGetMainByCountryId, vmCompo_Input_Price>();
        }
    }
}
