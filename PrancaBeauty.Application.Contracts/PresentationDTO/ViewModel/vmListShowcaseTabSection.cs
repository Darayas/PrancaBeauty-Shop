using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmListShowcaseTabSection
    {
        public string Id { get; set; }

        [Display(Name = "ParentName")]
        public string ParentName { get; set; }

        [Display(Name = "Name")]
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
