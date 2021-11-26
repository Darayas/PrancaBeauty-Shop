using Framework.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Categories
{
    public class InpGetParentsByChildId
    {
        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "ChildId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string ChildId { get; set; }
    }
}
