using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_VariantSearch
    {
        [Display(Name = "CategoryId")]
        [RequiredString]
        [GUID]
        public string CategoryId { get; set; }

        [Display(Name = "SelectedValues")]
        [MaxLengthString(1000)]
        public string SelectedValues { get; set; }
    }
}
