using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmListUsers
    {
        public string Id { get; set; }
        public string AccessLevelId { get; set; }

        [Display(Name = "ImgUrl")]
        public string ImgUrl { get; set; }

        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Display(Name = "AccessLevelName")]
        public string AccessLevelName { get; set; }

        [Display(Name = "RegDate")]
        public string Date { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "IsEmailConfirmed")]
        public bool IsEmailConfirmed { get; set; }

        [Display(Name = "IsPhoneNumberConfirmed")]
        public bool IsPhoneNumberConfirmed { get; set; }
    }
}
