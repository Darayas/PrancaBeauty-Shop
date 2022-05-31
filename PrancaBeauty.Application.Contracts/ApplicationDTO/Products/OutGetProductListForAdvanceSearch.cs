using System;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Products
{
    public class OutGetProductListForAdvanceSearch
    {
        public string Id { get; set; }
        public string[] ImgUrl { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public double OldPrice { get; set; }
        public double MainPrice { get; set; }
        public double PercentSavePrice { get; set; } // درصد تخفیفی که فروشنده تعیین کرده است
        public string CurrencySymbol { get; set; }
        public bool IsInBookmark { get; set; }
        public DateTime Date { get; set; }
        public int CountSell { get; set; } // برای مرتب سازی بر اساس محبوب ترین ها
        public double Rating { get; set; }
        public string Description { get; set; }
        public double KeywordSimilarity { get; set; }
        public string[] PropValues { get; set; }
        public string PropVals { get; set; }
    }
}
