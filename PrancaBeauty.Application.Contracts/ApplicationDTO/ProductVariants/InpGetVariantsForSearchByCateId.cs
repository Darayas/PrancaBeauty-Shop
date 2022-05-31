using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariants
{
    public class InpGetVariantsForSearchByCateId
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "CategoryId")]
        [RequiredString]
        [GUID]
        public string CategoryId { get; set; }
    }
}
