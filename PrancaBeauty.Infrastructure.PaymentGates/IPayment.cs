using PrancaBeauty.Infrastructure.PaymentGates.ZarinPal.Contracts;

namespace PrancaBeauty.Infrastructure.PaymentGates
{
    public interface IPayment
    {
        Task<OutZpPaymentVaryfication> PaymentVaryficationAsync(InpZpPaymentVaryfication Input);
        Task<OutZpStartPayment> StartPaymentAsync(InpZpStartPayment Input);
    }
}
