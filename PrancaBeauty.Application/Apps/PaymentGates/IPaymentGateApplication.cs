using PrancaBeauty.Application.Contracts.ApplicationDTO.PaymentGate;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.PaymentGates
{
    public interface IPaymentGateApplication
    {
        Task<OperationResult<OutGetGateData>> GetGateDataAsync(InpGetGateData Input);
        Task<List<OutGetPaymentGateByCountry>> GetPaymentGateByCountryAsync(InpGetPaymentGateByCountry Input);
    }
}