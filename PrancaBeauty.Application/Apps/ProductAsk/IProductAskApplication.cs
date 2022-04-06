using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProductAsks;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductAsk
{
    public interface IProductAskApplication
    {
        Task<OperationResult> AddNewAskAsync(InpAddNewAsk Input);
        Task<OperationResult> ChanageStatusAskAsync(InpChanageStatusAsk Input);
        Task<(OutPagingData PagingData, List<OutGetListAsks> LstAsks)> GetListAsksAsync(InpGetListAsks Input);
        Task<OperationResult> RemoveAskAsync(InpRemoveAsk Input);
    }
}