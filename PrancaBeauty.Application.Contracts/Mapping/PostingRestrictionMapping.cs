using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.PostingRestrictions;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class PostingRestrictionMapping : Profile
    {
        public PostingRestrictionMapping()
        {
            CreateMap<OutGetAllPostingRestrictionsByProductId, vmCompo_PostingRestrictions>();

        }
    }
}
