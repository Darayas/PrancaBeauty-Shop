﻿using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductTopics
{
    public class InpGetListForCombo
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "LangId")]
        [MaxLengthString(100)]
        public string Text { get; set; }
    }
}
