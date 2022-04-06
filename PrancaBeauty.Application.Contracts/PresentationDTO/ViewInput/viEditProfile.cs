using Framework.Common.DataAnnotations.String;
using System;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viEditProfile
    {
        [Display(Name = "FirstName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string LastName { get; set; }

        [Display(Name = "BirthDate")]
        public DateTime? BirthDate { get; set; }
    }
}
