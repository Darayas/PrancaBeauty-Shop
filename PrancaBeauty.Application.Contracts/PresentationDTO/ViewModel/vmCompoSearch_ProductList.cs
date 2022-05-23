using Framework.Common.Utilities.Paging;
using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompoSearch_ProductList
    {
        public string CategoryTitle { get; set; }
        public OutPagingData PagingData { get; set; }

        public List<vmCompoSearch_ProductListItems> LstProducts { get; set; } = new List<vmCompoSearch_ProductListItems>();
    }

    public class vmCompoSearch_ProductListItems
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
        public double Rating { get; set; }
        public string Description { get; set; }
    }
}
