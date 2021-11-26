using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Keywords
{
    public class InpGetIdByTitle
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string Title { get; set; }
    }
}
