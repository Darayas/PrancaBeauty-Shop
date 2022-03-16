using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ProductAsks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.ProductAsk
{
    public interface IProductAskApplication
    {
        Task<(OutPagingData PagingData, List<OutGetListAsks> LstAsks)> GetListAsksAsync(InpGetListAsks Input);
    }
}