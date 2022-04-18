﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTab
{
    public class InpGetShowcaseTabForEdit
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }
    }
}
