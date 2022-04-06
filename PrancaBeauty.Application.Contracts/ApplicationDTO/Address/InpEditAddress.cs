using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Address
{
    public class InpEditAddress
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "CountryId")]
        [RequiredString]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name = "ProvinceId")]
        [RequiredString]
        [GUID]
        public string ProvinceId { get; set; }

        [Display(Name = "CityId")]
        [RequiredString]
        [GUID]
        public string CityId { get; set; }

        [Display(Name = "District")]
        [RequiredString]
        [MaxLengthString(150)]
        public string District { get; set; } // محله

        [Display(Name = "Address")]
        [RequiredString]
        [MaxLengthString(500)]
        public string Address { get; set; }

        [Display(Name = "Plaque")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Plaque { get; set; } // پلاک

        [Display(Name = "Unit")]
        [MaxLengthString(100)]
        public string Unit { get; set; }

        [Display(Name = "PostalCode")]
        [RequiredString]
        [MaxLengthString(100)]
        public string PostalCode { get; set; }

        [Display(Name = "NationalCode")]
        [RequiredString]
        [MaxLengthString(100)]
        public string NationalCode { get; set; }

        [Display(Name = "FirstName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string LastName { get; set; }

        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [MaxLengthString(100)]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
