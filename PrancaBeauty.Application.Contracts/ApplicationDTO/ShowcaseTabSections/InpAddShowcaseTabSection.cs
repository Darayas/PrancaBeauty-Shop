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
    }
}