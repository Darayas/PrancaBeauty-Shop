namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Bills
{
    public class OutGetBillDetailsForPayment
    {
        public string Id { get; set; }
        public bool IsPayyed { get; set; }
        public double Amount { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string GateName { get; set; }

    }
}
