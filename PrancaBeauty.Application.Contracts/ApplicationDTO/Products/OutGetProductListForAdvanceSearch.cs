using Framework.Common.Utilities.Paging;
using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Products
{
    public class OutGetProductListForAdvanceSearch
    {
        public string CategoryTitle { get; set; }
        public OutPagingData Paging { get; set; }

        public List<OutGetProductListForAdvanceSearchItems> LstProducts { get; set; }
    }

    public class OutGetProductListForAdvanceSearchItems
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
    }
}
