﻿using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class InpGetUserProfileImgUrlById
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }
    }
}