using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductReviewsAttributes;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProductReviewsAttributeMapping : Profile
    {
        public ProductReviewsAttributeMapping()
        {
            CreateMap<OutGetAttributesByTopicId, viCompo_ProductReviewAttributes>();
        }
    }
}
