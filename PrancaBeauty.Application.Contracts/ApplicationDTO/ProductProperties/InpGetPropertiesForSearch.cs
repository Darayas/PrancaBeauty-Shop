using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductProperties
{
    public class InpGetPropertiesForSearch
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "TopicId")]
        [RequiredString]
        [GUID]
        public string TopicId { get; set; }
    }
}
