﻿using System;
using System.Collections.Generic;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase
{
    public class OutGetItemsForMainPageShowcase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string BackgroundColorCode { get; set; }
        public string CssStyle { get; set; }
        public string CssClass { get; set; }
        public int Sort { get; set; }
        public bool IsFullWidth { get; set; }


        public List<OutGetItemsForMainPageShowcase_Tab> LstTabs { get; set; }
    }

    public class OutGetItemsForMainPageShowcase_Tab
    {
        public string Title { get; set; }
        public string BackgroundColorCode { get; set; }
        public int Sort { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<OutGetItemsForMainPageShowcase_TabSection> LstTabSection { get; set; }
    }

    public class OutGetItemsForMainPageShowcase_TabSection
    {
        public int XlSize { get; set; } // Extra Larg
        public int LgSize { get; set; } // Larg 
        public int MdSize { get; set; } // Medium
        public int SmSize { get; set; } // Smal
        public int XsSize { get; set; } // Extra Small
        public bool IsSlider { get; set; }
        public int CountInSection { get; set; }
        public OutGetItemsForMainPageShowcase_TabSectionHowToDisplayEnum HowToDisplayItems { get; set; }

        public List<OutGetItemsForMainPageShowcase_TabSection> LstChilds { get; set; }
    }

    public enum OutGetItemsForMainPageShowcase_TabSectionHowToDisplayEnum
    {
        NoItem = 0, // ایتمی در سکشن قرار نمیگیرد
        FreeItem1 = 1, // تصویر بصورت FullWidth صفحه نمایش داده میشود و عنوان و کد در صورت وجود روی عکس به نمایش در خواهند آمد
        FreeItem2 = 2, // تصویر بزرگ میباشد و عنوان و کد، در صورت وجود زیر عکس نمایش داده میشود
        FreeItem3 = 3, // تصویر بزرگ میباشد و عنوان و کد، در صورت وجود بالای عکس نمایش داده میشود
        FreeItem4 = 4, // تصویر در یک باکس 1/4 در سمت راست و عنوان و کد در یک باکس 3/4 در سمت چپ
        FreeItem5 = 5, // تصویر در یک باکس 1/4 در سمت چپ و عنوان و کد در یک باکس 3/4 در سمت راست
        FreeItem6 = 6, // تصویر در یک باکس 7/12 در سمت راست و عنوان و کد در یک باکس 5/12 در سمت چپ
        FreeItem7 = 7, // تصویر در یک باکس 7/12 در سمت چپ و عنوان و کد در یک باکس 5/12 در سمت راست
        FreeItem8 = 8, // تصویر در یک باکس 6/12 در سمت راست و عنوان و کد در یک باکس 6/12 در سمت چپ
        FreeItem9 = 9, // تصویر در یک باکس 6/12 در سمت چپ و عنوان و کد در یک باکس 6/12 در سمت راست
        DefaultProduct = 10, // حالت معمولی محصول
    }
}
