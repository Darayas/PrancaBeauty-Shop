﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class InpGetFreeItemForEdit
    {
        [Display(Name = "SectionItemId")]
        [RequiredString]
        [GUID]
        public string SectionItemId { get; set; }
    }
}
