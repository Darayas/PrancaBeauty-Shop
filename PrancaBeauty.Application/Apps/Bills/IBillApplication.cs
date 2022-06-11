using PrancaBeauty.Application.Contracts.ApplicationDTO.Bills;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Bills
{
    public interface IBillApplication
    {
        Task<OperationResult> CreateBillFromCartAsync(InpCreateBillFromCart Input);
        Task<OutGetBillDetails> GetBillDetailsAsync(InpGetBillDetails Input);
    }
}