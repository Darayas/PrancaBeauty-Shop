using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Languages
{
    public class InpGetNativeNameByCode
    {
        [Display(Name = "Code")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(50, ErrorMessage = "MaxLengthMsg")]
        public string Code { get; set; }
    }

}
