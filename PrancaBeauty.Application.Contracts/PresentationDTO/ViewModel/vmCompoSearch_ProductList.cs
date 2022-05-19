using Framework.Common.Utilities.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompoSearch_ProductList
    {
        public string CategoryTitle { get; set; }
        public OutPagingData Paging { get; set; }

        public List<vmCompoSearch_ProductListItems> LstProducts { get; set; }
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
    }
}
