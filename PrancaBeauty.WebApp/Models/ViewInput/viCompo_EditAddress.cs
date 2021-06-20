using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_EditAddress
    {
        [Required(ErrorMessage = "RequiredMsg")]
        public string Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "CountryId")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string CountryId { get; set; }

        [Display(Name = "ProvinceId")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string ProvinceId { get; set; }

        [Display(Name = "CityId")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string CityId { get; set; }

        [Display(Name = "District")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string District { get; set; } // محله

        [Display(Name = "Address")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string Address { get; set; }

        [Display(Name = "Plaque")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string Plaque { get; set; } // پلاک

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "PostalCode")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string PostalCode { get; set; }

        [Display(Name = "NationalCode")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string NationalCode { get; set; }

        [Display(Name = "FirstName")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string FirstName { get; set; }

        [Display(Name = "LastName")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string LastName { get; set; }

        [Display(Name = "PhoneNumber")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string PhoneNumber { get; set; }
    }
}
