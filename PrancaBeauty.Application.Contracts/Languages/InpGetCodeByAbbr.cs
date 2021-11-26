using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Languages
{
    public class InpGetCodeByAbbr
    {
        [Display(Name = "Abbr")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(50, ErrorMessage = "MaxLengthMsg")]
        public string Abbr { get; set; }
    }
}
