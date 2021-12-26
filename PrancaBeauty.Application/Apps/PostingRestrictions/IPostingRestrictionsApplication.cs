using PrancaBeauty.Application.Contracts.PostingRestrictions;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.PostingRestrictions
{
    public interface IPostingRestrictionsApplication
    {
        Task<OperationResult> AddPostingRestrictionsToProductAsync(InpAddPostingRestrictionsToProduct Input);
        Task<OperationResult> EditPostingRestrictionsAsync(InpEditPostingRestrictions Input);
        Task<List<OutGetAllPostingRestrictionsByProductId>> GetAllPostingRestrictionsByProductIdAsync(InpGetAllPostingRestrictionsByProductId Input);
        Task<OperationResult> RemoveAllPostingRestrictionsFromProductAsync(InpRemoveAllPostingRestrictionsFromProduct Input);
    }
}