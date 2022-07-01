using Framework.Common.Utilities.Paging;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Bills;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Bills
{
    public interface IBillApplication
    {
        Task<OperationResult> ChangeBillAddressAsync(InpChangeBillAddress Input);
        Task<OperationResult> ChangeBillAuthorityAsync(InpChangeBillAuthority Input);
        Task<OperationResult> ChangeBillPaymentGateAsync(InpChangeBillPaymentGate Input);
        Task<OperationResult> CreateBillFromCartAsync(InpCreateBillFromCart Input);
        Task<OutGetBillDetails> GetBillDetailsAsync(InpGetBillDetails Input);
        Task<OutGetBillDetailsForPayment> GetBillDetailsForPaymentAsync(InpGetBillDetailsForPayment Input);
        Task<(OutPagingData PagingData, List<OutGetListBillForManage>)> GetListBillForManageAsync(InpGetListBillForManage Input);
        Task<OperationResult<OutStartPayment>> StartPaymentAsync(InpStartPayment Input);
    }
}