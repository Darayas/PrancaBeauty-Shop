using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.KeywordProducts;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class KeywordProductMapping : Profile
    {
        public KeywordProductMapping()
        {
            CreateMap<OutGetKeywordByProductId, vmCompo_Input_Keywords>();
        }
    }
}
