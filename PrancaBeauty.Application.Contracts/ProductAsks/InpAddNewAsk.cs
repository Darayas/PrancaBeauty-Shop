using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductAsks
{
    public class InpAddNewAsk
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "AskId")]
        [GUID]
        public string AskId { get; set; }

        [Display(Name = "AskText")]
        [RequiredString]
        [MaxLengthString(1000)]
        public string Text { get; set; }
    }
}
