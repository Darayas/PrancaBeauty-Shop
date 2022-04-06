﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viGetCompo_ProductSellers
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "VariaintValue")]
        [RequiredString]
        [MaxLengthString(100)]
        public string VariaintValue { get; set; }
    }
}