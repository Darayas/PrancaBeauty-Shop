using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class InpGetListShowcaseTabSectionItemForAdminPage
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "ShowcaseTabSectionId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabSectionId { get; set; }


        [Display(Name = "Title")]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue)]
        public int Page { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue)]
        public int Take { get; set; } = 10;
    }
}
