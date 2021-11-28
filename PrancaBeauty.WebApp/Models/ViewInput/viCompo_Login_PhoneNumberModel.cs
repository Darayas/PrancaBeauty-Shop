﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_Login_PhoneNumberModel
    {
        [Display(Name = "PhoneNumber")]
        [RequiredString]
        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
