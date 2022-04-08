using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProdcutReviews;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;
using System;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProductReviewMapping : Profile
    {
        public ProductReviewMapping()
        {
            CreateMap<OutGetReviewsForProductDetails, vmCompo_ListProductReviews>();
            CreateMap<OutGetReviewsForProductDetailsItems, vmCompo_ListProductReviewsItems>().ForMember(a => a.Date, a => a.MapFrom(b => b.Date.ToString("dd MMMM yyyy")));
            CreateMap<OutGetReviewsForProductDetailsMedia, vmCompo_ListProductReviewsMedia>();
            CreateMap<OutGetReviewsForProductDetailsAttributes, vmCompo_ListProductReviewsAttributes>().ForMember(a => a.Avg, a => a.MapFrom(b => Math.Round(b.Avg)));
            CreateMap<OutGetReviewsForProductDetailsSellers, vmGetReviewsForProductDetailsSellers>();
            CreateMap<viCompo_AddProductReviews, InpAddReviewFromUser>();
            CreateMap<viCompo_AddProductReviewsAttributes, InpAddReviewFromUserAttributes>();
        }
    }
}
