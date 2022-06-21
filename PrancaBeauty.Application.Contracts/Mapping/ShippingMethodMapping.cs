using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ShippingMethods;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ShippingMethodMapping : Profile
    {
        public ShippingMethodMapping()
        {
            CreateMap<OutGetShippingMethodForBill, vmCompo_ShippingMethods>();
        }
    }
}
