﻿using Framework.Application.Enums;
using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viEditProductSeller
    {
        //[Display(Name = "Id")]
        //[GUID]
        //public string Id { get; set; }

        [Display(Name = "SellerId")]
        [RequiredString]
        [GUID]
        public string SellerId { get; set; }
        
        [Display(Name = "ProductSellerId")]
        [RequiredString]
        [GUID]
        public string ProductSellerId { get; set; }

        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "VariantId")]
        [RequiredString]
        [GUID]
        public string VariantId { get; set; }


        public List<viEditProductSeller_Items> Variants { get; set; }
    }

    public class viEditProductSeller_Items
    {
        [Display(Name = "Id")]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "VariantTitle")]
        [RequiredIfNotDeleted(nameof(IsDelete))]
        [MaxLengthString(200)]
        public string Title { get; set; }

        [Display(Name = "VariantValueTitle")]
        [RequiredIfNotDeleted(nameof(IsDelete))]
        [MaxLengthString(200)]
        public string Value { get; set; }

        [Display(Name = "VariantPercent")]
        [RequiredIfNotDeleted(nameof(IsDelete))]
        [MaxLengthString(10)]
        public string Percent { get; set; }

        [Display(Name = "VariantGuaranteeId")]
        [RequiredIfNotDeleted(nameof(IsDelete))]
        [GUID]
        public string GuaranteeId { get; set; }

        [Display(Name = "ProductCode")]
        [RequiredIfNotDeleted(nameof(IsDelete))]
        [MaxLengthString(100)]
        public string ProductCode { get; set; }
        [Display(Name = "SendBy")]
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        [Display(Name = "SendFrom")]
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده

        [Display(Name = "CountInStock")]
        [NumRange(1, 100000, ErrorMessage = "RangeMsg")]
        public int CountInStock { get; set; }

        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }
    }
}
