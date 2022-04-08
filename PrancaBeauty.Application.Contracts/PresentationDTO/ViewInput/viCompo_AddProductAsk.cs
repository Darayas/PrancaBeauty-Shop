using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_AddProductAsk
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "AskId")]
        [GUID]
        public string AskId { get; set; }

        [Display(Name = "AskText")]
        [RequiredString]
        [MaxLengthString(1000)]
        public string Text { get; set; }
    }
}
