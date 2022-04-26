using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class InpSaveEditFreeItem
    {
        [Display(Name = "SectionItemId")]
        [RequiredString]
        [GUID]
        public string SectionItemId { get; set; }

        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }

        public List<InpSaveEditFreeItemTranslate> LstTranslate { get; set; }
    }

    public class InpSaveEditFreeItemTranslate
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
