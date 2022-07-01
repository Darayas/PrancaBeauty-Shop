namespace PrancaBeauty.Infrastructure.PaymentGates.ZarinPal.Contracts
{
    public class OutZpPaymentVaryfication
    {
        public int StatusCode { get; set; }
        public string TransactionNumber { get; set; }
    }
}
