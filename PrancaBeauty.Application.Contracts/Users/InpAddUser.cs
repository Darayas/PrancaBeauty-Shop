﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class InpAddUser
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
    }
}
