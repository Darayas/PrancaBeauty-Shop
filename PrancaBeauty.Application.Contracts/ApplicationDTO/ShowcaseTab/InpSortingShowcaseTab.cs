using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab
{
    public class InpSortingShowcaseTab
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
        public InpSortingShowcaseTabItem Act { get; set; }
    }

    public enum InpSortingShowcaseTabItem
    {
        Up = 0,
        Down = 1
    }
}
