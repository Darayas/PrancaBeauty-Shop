using Framework.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmMainPageShowcase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string BackgroundColorCode { get; set; }
        public string CssStyle { get; set; }
        public string CssClass { get; set; }
        public bool IsFullWidth { get; set; }


        public List<vmMainPageShowcase_Tab> LstTabs { get; set; }
    }

    public class vmMainPageShowcase_Tab
    {
        public string Title { get; set; }
        public string BackgroundColorCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<vmMainPageShowcase_TabSection> LstTabSection { get; set; }
    }

    public class vmMainPageShowcase_TabSection
    {
        public string ParentId { get; set; }
        public int XlSize { get; set; } // Extra Larg
        public int LgSize { get; set; } // Larg 
        public int MdSize { get; set; } // Medium
        public int SmSize { get; set; } // Smal
        public int XsSize { get; set; } // Extra Small
        public bool IsSlider { get; set; }
        public int CountInSection { get; set; }
        public TabSectionHowToDisplayEnum HowToDisplayItems { get; set; }

        public List<vmMainPageShowcase_SectionItem> LstSectionItem { get; set; }
    }

    public class vmMainPageShowcase_SectionItem
    {
        public TabSectionItemsEnum SectionType { get; set; }

        public vmMainPageShowcasee_SectionFreeItem FreeItem { get; set; }
        public vmMainPageShowcase_SectionProductItem ProductItem { get; set; }
        public List<vmMainPageShowcase_SectionProductItem> CategoryItems { get; set; }
        public List<vmMainPageShowcase_SectionProductItem> KeywordItems { get; set; }
    }

    public class vmMainPageShowcasee_SectionFreeItem
    {
        public string ImgUrl { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string HtmlText { get; set; }
    }

    public class vmMainPageShowcase_SectionProductItem
    {
        public string Id { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public double MainPrice { get; set; } // قیمت اصلی که از طرف نویسنده تعیین شده است
        public double SellerPercent { get; set; } // درصدی که فروشنده روی قیمت اصلی اعمال میکند
        public double PercentSavePrice { get; set; } // درصد تخفیفی که فروشنده تعیین کرده است
        public bool IsInBookmark { get; set; }
    }
}
