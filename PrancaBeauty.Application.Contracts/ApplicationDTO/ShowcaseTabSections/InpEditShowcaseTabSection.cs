using Framework.Common.DataAnnotations.String;
using Framework.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections
{
    public class InpEditShowcaseTabSection
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "ShowcaseTabId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabId { get; set; }

        [Display(Name = "ParentId")]
        [GUID]
        public string ParentId { get; set; }

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

        [Display(Name = "HowToDisplayItems")]
        public TabSectionHowToDisplayEnum HowToDisplay { get; set; }
    }
}
