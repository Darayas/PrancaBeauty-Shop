using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viAddShowcase
    {
        [Display(Name = "CountryId")]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }

        [Display(Name = "BackgroundColorCode")]
        [MaxLengthString(100)]
        public string BackgroundColorCode { get; set; }

        [Display(Name = "CssStyle")]
        [MaxLengthString(500)]
        public string CssStyle { get; set; }

        [Display(Name = "CssClass")]
        [MaxLengthString(100)]
        public string CssClass { get; set; }

        [Display(Name = "IsFullWidth")]
        public bool IsFullWidth { get; set; }

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        [Display(Name = "StartDate")]
        [RequiredString]
        public string StartDate { get; set; }

        [Display(Name = "EndDate")]
        public string EndDate { get; set; }

        public List<viAddShowcase_Translate> LstTranslate { get; set; }
    }

    public class viAddShowcase_Translate
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Title")]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [MaxLengthString(100)]
        public string Description { get; set; }
    }
}
