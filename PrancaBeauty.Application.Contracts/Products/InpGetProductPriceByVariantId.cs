﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Products
{
    public class InpGetProductPriceByVariantId
    {
        [Display(Name = "ProductVariantItemId")]
        [RequiredString]
        [GUID]
        public string ProductVariantItemId { get; set; }
    }
}