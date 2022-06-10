using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductGroups
{
    public class InpGetListProductGroupForCombo
    {
        [Display(Name = "TopicId")]
        [RequiredString]
        [GUID]
        public string TopicId { get; set; }

        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Text")]
        [MaxLengthString(100)]
        public string Text { get; set; }
    }
}
