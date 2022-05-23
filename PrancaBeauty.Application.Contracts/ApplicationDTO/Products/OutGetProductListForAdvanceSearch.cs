using System;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Products
{
    public class OutGetProductListForAdvanceSearch
    {
        public string Id { get; set; }
        public string[] ImgUrl { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public double MainPrice { get; set; } // قیمت اصلی که از طرف نویسنده تعیین شده است
        public double SellerPercent { get; set; } // درصدی که فروشنده روی قیمت اصلی اعمال میکند
        public double PercentSavePrice { get; set; } // درصد تخفیفی که فروشنده تعیین کرده است
        public string CurrencySymbol { get; set; }
        public bool IsInBookmark { get; set; }
        public DateTime Date { get; set; }
        public int CountSell { get; set; } // برای مرتب سازی بر اساس محبوب ترین ها
        public double Rating { get; set; }
        public string Description { get; set; }
    }
}
