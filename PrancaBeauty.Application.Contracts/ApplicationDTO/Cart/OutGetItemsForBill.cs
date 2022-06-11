using System;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Cart
{
    public class OutGetItemsForBill
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string VarianrItemId { get; set; }
        public string SellerId { get; set; }
        public string TaxGroupId { get; set; }
        public int Qty { get; set; }
    }
}
