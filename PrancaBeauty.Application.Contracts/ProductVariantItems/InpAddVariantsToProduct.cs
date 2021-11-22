using Framework.Application.Enums;
using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductVariantItems
{
    public class InpAddVariantsToProduct
    {
        [Display(Name = "ProductId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string ProductId { get; set; }
        public List<InpAddVariantsToProduct_Variants> Variants { get; set; }
    }

    public class InpAddVariantsToProduct_Variants
    {
        public string Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string Title { get; set; }

        [Display(Name = "Value")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string Value { get; set; }

        [Display(Name = "Percent")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string Percent { get; set; }

        [Display(Name = "GuaranteeId")]
        [GUID(ErrorMessage = "GUIDMsg")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string GuaranteeId { get; set; }

        [Display(Name = "ProductCode")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string ProductCode { get; set; }

        [Display(Name = "SendBy")]
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        [Display(Name = "SendFrom")]
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده

        [Display(Name = "CountInStock")]
        [Range(1, 100000, ErrorMessage = "RangeMsg")]
        public int CountInStock { get; set; }
    }
}
