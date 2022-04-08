using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductAsks;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class ProductAskMapping : Profile
    {
        public ProductAskMapping()
        {
            CreateMap<viCompo_AddProductAsk, InpAddNewAsk>();
            CreateMap<OutGetListAsks, vmCompo_ListProductAsks>();
            CreateMap<OutGetListAsks_Answer, vmGetListAsks_Answer>();
        }
    }
}
