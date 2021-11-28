using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viRegisterModel
    {
        [Display(Name = "Email")]
        [RequiredString]
        [MaxLengthString(100)]
        [TemplEmail]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [MaxLengthString(30)]
        [PhoneNumber]
        public string PhoneNumber { get; set; }

        [Display(Name = "FirstName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string LastName { get; set; }

        [Display(Name = "Password")]
        [RequiredString]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword")]
        [RequiredString]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "PassCompareMsg")]
        public string ConfirmPassword { get; set; }
    }
}
