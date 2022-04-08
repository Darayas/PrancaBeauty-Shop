using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariants;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProductVariantsMapping : Profile
    {
        public ProductVariantsMapping()
        {
            CreateMap<outGetProductVariantsLstForCombo, vmCompo_Combo_ProductVariants>();
        }
    }
}
