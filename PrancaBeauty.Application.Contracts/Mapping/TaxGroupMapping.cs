using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.TaxGroup;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class TaxGroupMapping : Profile
    {
        public TaxGroupMapping()
        {
            CreateMap<OutGetListTaxGroupForCombo, vmCompo_Combo_TaxGroups>();
        }
    }
}
