using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viListShowcaseTabSorting
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "ShowcaseId")]
        [RequiredString]
        [GUID]
        public string ShowcaseId { get; set; }

        [Display(Name = "Action")]
        [RequiredString]
        public viListShowcaseTabSortingItem Act { get; set; }
    }

    public enum viListShowcaseTabSortingItem
    {
        Up = 0,
        Down = 1
    }
}
