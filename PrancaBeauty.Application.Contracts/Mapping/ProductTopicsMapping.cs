using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductTopics;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProductTopicsMapping : Profile
    {
        public ProductTopicsMapping()
        {
            CreateMap<OutGetProductTopicsListForCombo, vmCompo_Combo_Topics>();

        }
    }
}
