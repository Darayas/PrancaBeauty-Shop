using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.Address;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Address
{
    public interface IAddressApplication
    {
        Task<(OutPagingData, List<GetAddressByUserIdForManage>)> GetAddressByUserIdForManageAsync(string UserId, string LangId, string Search, int PageNum, int Take);
    }
}