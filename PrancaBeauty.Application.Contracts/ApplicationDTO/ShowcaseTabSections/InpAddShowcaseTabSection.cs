﻿using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections
{
    public class InpAddShowcaseTabSection
    {
        [Display(Name = "ParentId")]
        [GUID]
        public string ParentId { get; set; }

        [Display(Name = "ShowcaseTabId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabId { get; set; }

        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }

        [Display(Name = "XlSize")]
        [NumRange(1, 12)]
        public int XlSize { get; set; } // Extra Larg

        [Display(Name = "LgSize")]
        [NumRange(1, 12)]
        public int LgSize { get; set; } // Larg 

        [Display(Name = "MdSize")]
        [NumRange(1, 12)]
        public int MdSize { get; set; } // Medium

        [Display(Name = "SmSize")]
        [NumRange(1, 12)]
        public int SmSize { get; set; } // Smal

        [Display(Name = "XsSize")]
        [NumRange(1, 12)]
        public int XsSize { get; set; } // Extra Small

        [Display(Name = "IsSlider")]
        public bool IsSlider { get; set; }

        [Display(Name = "CountInSection")]
        public int CountInSection { get; set; }

        [Display(Name = "HowToDisplayItems")]
        public InpAddShowcaseTabSectionHowToDisplayEnum HowToDisplay { get; set; }
    }

    public enum InpAddShowcaseTabSectionHowToDisplayEnum
    {
        FreeItem1, // تصویر بصورت FullWidth صفحه نمایش داده میشود و عنوان و کد در صورت وجود روی عکس به نمایش در خواهند آمد
        FreeItem2, // تصویر بزرگ میباشد و عنوان و کد، در صورت وجود زیر عکس نمایش داده میشود
        FreeItem3, // تصویر بزرگ میباشد و عنوان و کد، در صورت وجود بالای عکس نمایش داده میشود
        FreeItem4, // تصویر در یک باکس 1/4 در سمت راست و عنوان و کد در یک باکس 3/4 در سمت چپ
        FreeItem5, // تصویر در یک باکس 1/4 در سمت چپ و عنوان و کد در یک باکس 3/4 در سمت راست
        FreeItem6, // تصویر در یک باکس 7/12 در سمت راست و عنوان و کد در یک باکس 5/12 در سمت چپ
        FreeItem7, // تصویر در یک باکس 7/12 در سمت چپ و عنوان و کد در یک باکس 5/12 در سمت راست
        FreeItem8, // تصویر در یک باکس 6/12 در سمت راست و عنوان و کد در یک باکس 6/12 در سمت چپ
        FreeItem9, // تصویر در یک باکس 6/12 در سمت چپ و عنوان و کد در یک باکس 6/12 در سمت راست
    }
}
