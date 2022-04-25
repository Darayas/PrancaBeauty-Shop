using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viAddSectionCategoryItem
    {
        [Display(Name = "ShowcaseTabSectionId")]
        [RequiredString]
        [GUID]
        public string ShowcaseTabSectionId { get; set; }

        [Display(Name = "CategoryId")]
        [RequiredString]
        [GUID]
        public string CategoryId { get; set; }

        [Display(Name = "CountFetch")]
        [RequiredString]
        [NumRange(1, 30)]
        public int CountFetch { get; set; }

        [Display(Name = "OrderBy")]
        public viAddSectionCategoryItemEnum OrderBy { get; set; }
    }

    public enum viAddSectionCategoryItemEnum
    {
        Newest,
        Oldest,
        Popular
    }
}
