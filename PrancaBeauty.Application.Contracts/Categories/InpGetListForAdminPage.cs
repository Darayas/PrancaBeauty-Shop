using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Categories
{
    public class InpGetListForAdminPage
    {
        [Display(Name = "PageNum")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(150, ErrorMessage = "MaxLengthMsg")]
        public string Title { get; set; }

        [Display(Name = "ParentTitle")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(150, ErrorMessage = "MaxLengthMsg")]
        public string ParentTitle { get; set; }

        [Display(Name = "PageNum")]
        [Range(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int PageNum { get; set; }

        [Display(Name = "Take")]
        [Range(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Take { get; set; }
    }
}
