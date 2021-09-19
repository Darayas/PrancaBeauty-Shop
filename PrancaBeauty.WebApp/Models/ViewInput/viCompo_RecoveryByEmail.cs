﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_RecoveryByEmail
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "RequiredMsg")]
        [EmailAddress(ErrorMessage = "EmailAddressMsg")]
        public string Email { get; set; }
    }
}
