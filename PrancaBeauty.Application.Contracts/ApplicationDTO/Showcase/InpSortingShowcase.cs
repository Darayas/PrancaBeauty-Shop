using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase
{
    public class InpSortingShowcase
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "Action")]
        [RequiredString]
        public InpSortingShowcaseSortingItem Act { get; set; }
    }

    public enum InpSortingShowcaseSortingItem
    {
        Up = 0,
        Down = 1
    }
}
