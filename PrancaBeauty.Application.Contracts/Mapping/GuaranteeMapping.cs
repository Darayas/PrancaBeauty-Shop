using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Guarantee;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class GuaranteeMapping : Profile
    {
        public GuaranteeMapping()
        {
            CreateMap<OutGetGuaranteeListForCombo, vmCompo_Combo_Guarantee>();

        }
    }
}
