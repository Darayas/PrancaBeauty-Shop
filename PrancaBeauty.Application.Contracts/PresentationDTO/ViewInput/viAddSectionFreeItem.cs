using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viAddSectionFreeItem
    {
        [Display(Name = "ShowcaseTabSectionItemId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabSectionId { get; set; }

        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }

        public List<viAddSectionFreeItemTranslate> LstTranslate { get; set; }
    }

    public class viAddSectionFreeItemTranslate
    {
        [Display(Name = "Name")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "FileId")]
        [RequiredString]
        [GUID]
        public string FileId { get; set; }

        [Display(Name = "Title")]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "Url")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Url { get; set; }

        [Display(Name = "HtmlCode")]
        [MaxLengthString(100)]
        public string HtmlText { get; set; }
    }
}
