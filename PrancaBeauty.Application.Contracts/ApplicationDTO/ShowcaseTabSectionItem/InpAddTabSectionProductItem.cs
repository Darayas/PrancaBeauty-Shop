using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class InpAddTabSectionProductItem
    {
        [Display(Name = "ShowcaseTabSectionItemId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabSectionId { get; set; }

        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
    }
}
