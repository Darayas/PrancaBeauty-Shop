using Framework.Common.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class InpAddNewAccessLevel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(150, ErrorMessage = "MaxLengthMsg")]
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}
