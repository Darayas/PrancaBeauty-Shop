namespace PrancaBeauty.Infrastructure.PaymentGates.ZarinPal.Contracts
{
    public class OutZpStartPayment
    {
        public int StatusCode { get; set; }
        public string Authority { get; set; }
        public string PaymentyGateUrl { get; set; }
    }
}
