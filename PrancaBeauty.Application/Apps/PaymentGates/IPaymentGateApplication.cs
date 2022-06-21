using PrancaBeauty.Application.Contracts.ApplicationDTO.PaymentGate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.PaymentGates
{
    public interface IPaymentGateApplication
    {
        Task<List<OutGetPaymentGateByCountry>> GetPaymentGateByCountryAsync(InpGetPaymentGateByCountry Input);
    }
}