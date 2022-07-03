using PrancaBeauty.Application.Contracts.ApplicationDTO.BillItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.BillItems
{
    public interface IBillItemsApplication
    {
        Task<OperationResult> BillItemFinalPriceRegistrationAsync(InpBillItemFinalPriceRegistration Input);
    }
}