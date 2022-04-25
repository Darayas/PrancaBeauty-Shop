using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Keywords;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class KeywordMapping : Profile
    {
        public KeywordMapping()
        {
            CreateMap<OutGetKeywordListForCombo, vmCompo_Combo_Keywords>();
        }
    }
}
