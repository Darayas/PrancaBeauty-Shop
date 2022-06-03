using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCart
    {
        public int CountInCart { get; set; }
        public double TotalAmount { get; set; }
        public string CurrencySymbol { get; set; }
        public double TaxAmount { get; set; }
        public double ShippingAmount { get; set; }

        public List<vmCartItems> Items { get; set; }
    }

    public class vmCartItems
    {
        public string Id { get; set; }
        public string ProductImgUrl { get; set; }
        public string ProductName { get; set; }
        public string ProductTitle { get; set; }
        public double OldPrice { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public double PercentSavePrice { get; set; }
        public double TaxPercent { get; set; }
        public string CurrencySymbol { get; set; }
        public int Qty { get; set; }
        public string VariantTopic { get; set; }
        public string VariantTitle { get; set; }
    }
}
