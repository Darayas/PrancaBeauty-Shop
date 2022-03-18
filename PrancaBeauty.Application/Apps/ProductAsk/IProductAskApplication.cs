using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ProductAsks;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductAsk
{
    public interface IProductAskApplication
    {
        Task<OperationResult> AddNewAskAsync(InpAddNewAsk Input);
        Task<(OutPagingData PagingData, List<OutGetListAsks> LstAsks)> GetListAsksAsync(InpGetListAsks Input);
    }
}