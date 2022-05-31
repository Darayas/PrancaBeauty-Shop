using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariants;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProductVariantMapping : Profile
    {
        public ProductVariantMapping()
        {
            CreateMap<viEditProductSeller, InpEditProductVariants>();
            CreateMap<viEditProductSeller_Items, InpEditProductVariants_Variants>();
            CreateMap<OutGetAllVariantsByProductId, vmGetProductSellerVariants>();
            CreateMap<OutGetAllProductVariantsForProductDetails, vmCompo_ProductVariantItem>();
            CreateMap<OutGetAllProductVariantsForProductDetailsItem, vmLstCompo_ProductVariantItem>();
            CreateMap<OutGetAllVariantsByProductId, vmCompo_Variants>();
            CreateMap<outGetProductVariantsLstForCombo, vmCompo_Combo_ProductVariants>();

            CreateMap<OutGetVariantsForSearchByCateId, vmCompo_VariantSearch>();
            CreateMap<OutGetVariantsForSearchByCateIdItem, vmCompo_VariantSearchItem>();
        }
    }
}
