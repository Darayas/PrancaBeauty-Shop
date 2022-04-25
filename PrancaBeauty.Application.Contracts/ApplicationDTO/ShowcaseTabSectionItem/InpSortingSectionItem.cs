using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class InpSortingSectionItem
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
        public InpSortingSectionItemItem Act { get; set; }
    }

    public enum InpSortingSectionItemItem
    {
        Up = 0,
        Down = 1
    }
}
