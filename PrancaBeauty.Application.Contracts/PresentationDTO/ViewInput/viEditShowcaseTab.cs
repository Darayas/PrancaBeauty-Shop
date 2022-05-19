using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viEditShowcaseTab
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "ShowcaseId")]
        [RequiredString]
        [GUID]
        public string ShowcaseId { get; set; }

        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }

        [Display(Name = "BackgroundColorCode")]
        [MaxLengthString(100)]
        public string BackgroundColorCode { get; set; }

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        [Display(Name = "StartDate")]
        [RequiredString]
        // TODO DA: DateTime
        public string StartDate { get; set; }

        [Display(Name = "EndDate")]
        // TODO DA: DateTime
        public string EndDate { get; set; }

        [Display(Name = "LangsBold:")]
        public List<viEditShowcaseTab_Translate> LstTranslate { get; set; }
    }

    public class viEditShowcaseTab_Translate
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Title { get; set; }
    }
}
