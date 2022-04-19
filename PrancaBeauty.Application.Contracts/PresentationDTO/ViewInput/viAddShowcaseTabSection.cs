using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viAddShowcaseTabSection
    {
        [Display(Name= "ParentId")]
        [RequiredString]
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
        public int XlSize { get; set; } // Extra Larg

        [Display(Name = "LgSize")]
        public int LgSize { get; set; } // Larg 

        [Display(Name = "MdSize")]
        public int MdSize { get; set; } // Medium

        [Display(Name = "SmSize")]
        public int SmSize { get; set; } // Smal

        [Display(Name = "XsSize")]
        public int XsSize { get; set; } // Extra Small

        [Display(Name = "IsSlider")]
        public bool IsSlider { get; set; }

        [Display(Name = "CountInSection")]
        public int CountInSection { get; set; }
    }
}
