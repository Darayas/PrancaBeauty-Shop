using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.PaymentGate;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class PaymentGateMapping : Profile
    {
        public PaymentGateMapping()
        {
            CreateMap<OutGetPaymentGateByCountry, vmCompo_HowToPay>();
        }
    }
}
