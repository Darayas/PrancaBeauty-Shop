using Framework.Application.Enums;
using Framework.Common.DataAnnotations;
using Framework.Common.DataAnnotations.String;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viAddProduct
    {

        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "TopicId")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        [GUID]
        public string TopicId { get; set; }

        [Display(Name = "CategoryId")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        [GUID]
        public string CategoryId { get; set; }
        
        [Display(Name = "VariantId")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        [GUID]
        public string VariantId { get; set; }

        [Display(Name = "ProductName")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        [ItsForUrl(ErrorMessage = "ItsForUrlMsg")]
        public string Name { get; set; } // Uniqe Name

        [Display(Name = "ProductTitle")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        [ItsForProductTitle(ErrorMessage = "ItsForProductTitleMsg")]
        public string Title { get; set; }

        [Display(Name = "ProductDate")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        public string Date { get; set; }

        [Display(Name = "ProductPrice")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        public string Price { get; set; }

        [Display(Name = "MetaTagKeyword")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string MetaTagKeyword { get; set; }

        [Display(Name = "MetaTagCanonical")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string MetaTagCanonical { get; set; }

        [Display(Name = "MetaTagDescreption")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string MetaTagDescreption { get; set; }

        [Display(Name = "ProductDescription")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        public string Description { get; set; }

        [Display(Name = "IsDraft")]
        public bool IsDraft { get; set; }

        [Display(Name = "Properties")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        public List<viAddProduct_Properties> Properties { get; set; }

        [Display(Name = "Keywords")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        public List<viAddProduct_Keywords> Keywords { get; set; }

        [Display(Name = "ProductVariants")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        public List<viAddProduct_Variants> Variants { get; set; }

        [Display(Name = "PostingRestrictions")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        public List<viAddProduct_PostingRestrictions> PostingRestrictions { get; set; }
    }

    public class viAddProduct_Properties
    {
        [Display(Name ="PropertyId")]
        [GUID]
        [Required(ErrorMessage = "Required")]
        public string Id { get; set; }

        [Display(Name = "Value")]
        [RequiredForDraft("IsDraft", ErrorMessage = "RequiredMsg")]
        [StringLength(3000, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string Value { get; set; }
    }

    public class viAddProduct_Keywords
    {
        public string Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(250, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string Title { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string Similarity { get; set; }
    }

    public class viAddProduct_Variants
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
        [GUID]
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
        [Range(1, 100000,ErrorMessage = "RangeMsg")]
        public int CountInStock { get; set; }
    }

    public class viAddProduct_PostingRestrictions
    {
        public string Id { get; set; }

        [Display(Name = "CountryId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name = "PostingStatus")]
        public bool Posting { get; set; }
    }
}
