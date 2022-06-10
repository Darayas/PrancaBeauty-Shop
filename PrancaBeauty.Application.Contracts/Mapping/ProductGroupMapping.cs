using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductGroups;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProductGroupMapping : Profile
    {
        public ProductGroupMapping()
        {
            CreateMap<OutGetListProductGroupForCombo, vmCompo_Combo_TaxGroups>();
        }
    }
}
