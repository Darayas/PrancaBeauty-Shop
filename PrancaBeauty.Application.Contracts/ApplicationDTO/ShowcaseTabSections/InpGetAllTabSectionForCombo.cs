using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections
{
    public class InpGetAllTabSectionForCombo
    {
        [Display(Name = "ShowcaseTabId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabId { get; set; }

        [Display(Name = "Name")]
        [MaxLengthString(100)]
        public string Name { get; set; }
    }
}
