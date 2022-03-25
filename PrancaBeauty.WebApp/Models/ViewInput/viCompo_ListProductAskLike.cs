﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ListProductAskLike
    {
        [Display(Name = "AskId")]
        [RequiredString]
        [GUID]
        public string AskId { get; set; }
    }
}
