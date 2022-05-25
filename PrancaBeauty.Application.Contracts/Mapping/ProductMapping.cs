using AutoMapper;
using Framework.Common.ExMethods;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Products;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Globalization;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<OutGetProductsForManage, vmProductList>();

            CreateMap<OutGetProductListForCombo, vmCompo_Combo_Products>();

            CreateMap<viAddProduct, InpAddProdcut>();
            CreateMap<viAddProduct_Properties, InpAddProduct_Properties>();
            CreateMap<viAddProduct_Keywords, InpAddProduct_Keywords>();
            CreateMap<viAddProduct_Variants, InpAddProduct_Variants>();
            CreateMap<viAddProduct_PostingRestrictions, InpAddProduct_PostingRestrictions>();

            CreateMap<OutGetProductForEdit, viEditProduct>().ForMember(x => x.Date,
                                                                       opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"))));
            CreateMap<viEditProduct, InpSaveEditProduct>();
            CreateMap<viEditProduct_Properties, InpSaveEditProduct_Properties>();
            CreateMap<viEditProduct_Keywords, InpSaveEditProduct_Keywords>();
            CreateMap<viEditProduct_Variants, InpSaveEditProduct_Variants>();
            CreateMap<viEditProduct_PostingRestrictions, InpSaveEditProduct_PostingRestrictions>();

            CreateMap<OutGetProductForDetails, vmProductDetails>();
            CreateMap<OutGetProductForDetails_Media, vmProductDetails_Media>();
            CreateMap<OutGetProductForDetails_Properties, vmProductDetails_Properties>();

            CreateMap<viCompoSearch_ProductList, InpGetProductListForAdvanceSearch>();
            CreateMap<OutGetProductListForAdvanceSearch, vmCompoSearch_ProductListItems>();

            CreateMap<OutGetProductPriceByVariantId, vmCompo_ProductSelectedPrice>()
                                                                .ForMember(a => a.ProductOldPrice, a => a.MapFrom(b => b.ProductOldPrice.ToN3()))
                                                                .ForMember(a => a.ProductPrice, a => a.MapFrom(b => b.ProductPrice.ToN3()));
        
        }
    }
}
