using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductVariantItems
{
    public class InpCheckDuplicateForUser
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "SellerId")]
        [RequiredString]
        [GUID]
        public string ProductSellerId { get; set; }

        [Display(Name = "ProductVariantCode")]
        [RequiredString]
        [MaxLengthString(200)]
        public string ProductVariantCode { get; set; }

        [Display(Name = "VariantTitle")]
        [RequiredString]
        [MaxLengthString(200)]
        public string VariantTitle { get; set; }

        [Display(Name = "VariantValue")]
        [RequiredString]
        [MaxLengthString(100)]
        public string VariantValue { get; set; }
    }
}
