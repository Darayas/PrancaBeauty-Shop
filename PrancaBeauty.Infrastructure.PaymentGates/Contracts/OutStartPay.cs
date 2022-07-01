namespace PrancaBeauty.Infrastructure.PaymentGates.Contracts
{
    public class OutStartPay
    {
        public int StatusCode { get; set; }
        public string Authority { get; set; }
        public string PaymentyGateUrl { get; set; }
    }
}
