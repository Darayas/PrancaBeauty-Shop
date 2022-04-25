using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viListSectionItemSorting
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "TabSectionId")]
        [RequiredString]
        [GUID]
        public string TabSectionId { get; set; }

        [Display(Name = "Action")]
        [RequiredString]
        public viListSectionItemSortingEnum Act { get; set; }
    }

    public enum viListSectionItemSortingEnum
    {
        Up = 0,
        Down = 1
    }
}
