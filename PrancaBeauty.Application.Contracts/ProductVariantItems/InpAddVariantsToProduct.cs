using Framework.Application.Enums;
using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductVariantItems
{
    public class InpAddVariantsToProduct
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "ProductSellerId")]
        [RequiredString]
        [GUID]
        public string ProductSellerId { get; set; }

        [Display(Name = "VariantId")]
        [RequiredString]
        [GUID]
        public string VariantId { get; set; }


        public List<InpAddVariantsToProduct_Variants> Variants { get; set; }
    }

    public class InpAddVariantsToProduct_Variants
    {

        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(200)]
        public string Title { get; set; }

        [Display(Name = "Value")]
        [RequiredString]
        [MaxLengthString(200)]
        public string Value { get; set; }

        [Display(Name = "Percent")]
        [RequiredString]
        [MaxLengthString(10)]
        public string Percent { get; set; }

        [Display(Name = "GuaranteeId")]
        [RequiredString]
        [GUID]
        public string GuaranteeId { get; set; }

        [Display(Name = "ProductCode")]
        [RequiredString]
        [MaxLengthString(100)]
        public string ProductCode { get; set; }

        [Display(Name = "SendBy")]
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        public bool IsDelete { get; set; }

        [Display(Name = "SendFrom")]
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده

        [Display(Name = "CountInStock")]
        [NumRange(1, 100000)]
        public int CountInStock { get; set; }
    }
}
