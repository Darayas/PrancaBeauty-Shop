﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viSearch
    {
        [Display(Name = "CategoryName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string CategoryName { get; set; }

        [Display(Name = "KeywordTitle")]
        [MaxLengthString(100)]
        public string KeywordTitle { get; set; }
    }
}
