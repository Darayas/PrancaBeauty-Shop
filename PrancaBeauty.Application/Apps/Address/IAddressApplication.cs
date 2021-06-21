using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.Address;
using PrancaBeauty.Application.Contracts.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Address
{
    public interface IAddressApplication
    {
        Task<OperationResult> AddAddressAsync(InpAddAddress Input);
        Task<OperationResult> EditAddressAsync(InpEditAddress Input);
        Task<(OutPagingData, List<OutGetAddressByUserIdForManage>)> GetAddressByUserIdForManageAsync(string UserId, string LangId, string Search, int PageNum, int Take);
        Task<OutGetAddressDetails> GetAddressDetailsAsync(string UserId, string Id);
        Task<OperationResult> RemoveAddressAsync(string UserId, string Id);
    }
}