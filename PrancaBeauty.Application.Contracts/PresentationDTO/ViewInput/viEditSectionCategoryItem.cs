using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viEditSectionCategoryItem
    {

        [Display(Name = "SectionItemId")]
        [RequiredString]
        [GUID]
        public string SectionItemId { get; set; }

        [Display(Name = "CategoryId")]
        [RequiredString]
        [GUID]
        public string CategoryId { get; set; }

        public int CountFetch { get; set; }

        public viEditSectionCategoryItemOrderByEnum OrderBy { get; set; }
    }

    public enum viEditSectionCategoryItemOrderByEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
