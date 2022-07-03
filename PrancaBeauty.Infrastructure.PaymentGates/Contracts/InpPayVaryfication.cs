namespace PrancaBeauty.Infrastructure.PaymentGates.Contracts
{
    public class InpPayVaryfication
    {
        public Dictionary<string, string> QueryData { get; set; }
        public double Amount { get; set; }
    }
}
