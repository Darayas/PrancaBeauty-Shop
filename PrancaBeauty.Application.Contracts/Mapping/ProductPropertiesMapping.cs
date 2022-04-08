using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductProperties;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProductPropertiesMapping : Profile
    {
        public ProductPropertiesMapping()
        {
            CreateMap<OutGetForManageProduct, vmCompo_Properties>();
        }
    }
}
