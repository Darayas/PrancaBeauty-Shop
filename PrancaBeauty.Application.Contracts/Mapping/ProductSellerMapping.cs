using AutoMapper;
using Framework.Common.ExMethods;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductSellers;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System.Globalization;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProductSellerMapping : Profile
    {
        public ProductSellerMapping()
        {
            CreateMap<vmGetAllSellerForManageByProductId, vmListSellers>().ForMember(x => x.Date,
                                                                                     opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")));

            CreateMap<viAddProductSeller, InpAddSellerWithVariantsToProdcut>();
            CreateMap<viAddProductSeller_Items, InpAddSellerWithVariantsToProdcut_Variants>();

            CreateMap<OutGetListSellerByVariantValue, vmCompo_ProductSellers>()
                                                                 .ForMember(a => a.MainPrice, a => a.MapFrom(b => b.MainPrice.ToN3()))
                                                                 .ForMember(a => a.PercentSavePrice, a => a.MapFrom(b => b.PercentSavePrice.ToString(new CultureInfo("en-US"))))
                                                                 .ForMember(a => a.OldPrice, a => a.MapFrom(b => b.OldPrice.ToN3()));
        }
    }
}
