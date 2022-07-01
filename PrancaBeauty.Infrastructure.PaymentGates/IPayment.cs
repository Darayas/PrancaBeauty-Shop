using PrancaBeauty.Infrastructure.PaymentGates.Contracts;

namespace PrancaBeauty.Infrastructure.PaymentGates
{
    public interface IPayment
    {
        Task<OutPayVaryfication> PaymentVaryficationAsync(InpPayVaryfication Input);
        Task<OutStartPay> StartPaymentAsync(InpStartPay Input);
    }
}
