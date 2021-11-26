using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_EditAddress
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "CountryId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name = "ProvinceId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string ProvinceId { get; set; }

        [Display(Name = "CityId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string CityId { get; set; }

        [Display(Name = "District")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(150,ErrorMessage = "MaxLength")]
        public string District { get; set; } // محله

        [Display(Name = "Address")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(500, ErrorMessage = "MaxLength")]
        public string Address { get; set; }

        [Display(Name = "Plaque")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string Plaque { get; set; } // پلاک

        [Display(Name = "Unit")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string Unit { get; set; }

        [Display(Name = "PostalCode")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string PostalCode { get; set; }

        [Display(Name = "NationalCode")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string NationalCode { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        public string LastName { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLength")]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
