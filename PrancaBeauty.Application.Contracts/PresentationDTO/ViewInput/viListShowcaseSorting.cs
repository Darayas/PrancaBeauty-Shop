using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viListShowcaseSorting
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "Action")]
        [RequiredString]
        public viListShowcaseSortingItem Act { get; set; }
    }

    public enum viListShowcaseSortingItem
    {
        Up = 0,
        Down = 1
    }
}
