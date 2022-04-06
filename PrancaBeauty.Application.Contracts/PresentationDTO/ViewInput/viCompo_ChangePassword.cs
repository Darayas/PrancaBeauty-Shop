using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_ChangePassword
    {
        [Display(Name = "CurrentPassword")]
        [RequiredString]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "NewPassword")]
        [RequiredString]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "ConfirmPassword")]
        [RequiredString]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "CompareMsg")]
        public string ConfirmPassword { get; set; }

    }
}
