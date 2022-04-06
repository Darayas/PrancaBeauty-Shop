using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_AddAddress
    {
        public string UserId { get; set; }

        [Display(Name = "CountryId")]
        [RequiredString]
        public string CountryId { get; set; }

        [Display(Name = "ProvinceId")]
        [RequiredString]
        public string ProvinceId { get; set; }

        [Display(Name = "CityId")]
        [RequiredString]
        public string CityId { get; set; }

        [Display(Name = "District")]
        [RequiredString]
        public string District { get; set; } // محله

        [Display(Name = "Address")]
        [RequiredString]
        public string Address { get; set; }

        [Display(Name = "Plaque")]
        [RequiredString]
        public string Plaque { get; set; } // پلاک

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "PostalCode")]
        [RequiredString]
        public string PostalCode { get; set; }

        [Display(Name = "NationalCode")]
        [RequiredString]
        public string NationalCode { get; set; }

        [Display(Name = "FirstName")]
        [RequiredString]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [RequiredString]
        public string LastName { get; set; }

        [Display(Name = "PhoneNumber")]
        [RequiredString]
        public string PhoneNumber { get; set; }
    }
}
