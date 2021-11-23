using Framework.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.KeywordProducts
{
    public class InpRemoveAllProductKeywords
    {
        [Display(Name = "ProductId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string ProductId { get; set; }
    }
}
