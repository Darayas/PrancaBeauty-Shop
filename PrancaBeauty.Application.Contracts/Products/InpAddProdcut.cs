using Framework.Application.Enums;
using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Products
{
    public class InpAddProdcut
    {
        [Display(Name = "AuthorUserId")]
        [RequiredString]
        [GUID]
        public string AuthorUserId { get; set; }

        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "TopicId")]
        [RequiredForDraft(nameof(IsDraft))]
        [GUID]
        public string TopicId { get; set; }

        [Display(Name = "CategoryId")]
        [RequiredForDraft(nameof(IsDraft))]
        [GUID]
        public string CategoryId { get; set; }

        [Display(Name = "VariantId")]
        [RequiredForDraft(nameof(IsDraft))]
        [GUID]
        public string VariantId { get; set; }

        [Display(Name = "ProductName")]
        [RequiredForDraft(nameof(IsDraft))]
        [MaxLengthString(250)]
        [ItsForUrl]
        public string Name { get; set; } // Uniqe Name

        [Display(Name = "ProductTitle")]
        [RequiredString]
        [MaxLengthString(250)]
        [ItsForProductTitle]
        public string Title { get; set; }

        [Display(Name = "ProductDate")]
        [RequiredForDraft(nameof(IsDraft))]
        public string Date { get; set; }

        [Display(Name = "ProductPrice")]
        [RequiredForDraft(nameof(IsDraft))]
        public string Price { get; set; }

        [Display(Name = "MetaTagKeyword")]
        [RequiredForDraft(nameof(IsDraft))]
        [MaxLengthString(300)]
        public string MetaTagKeyword { get; set; }

        [Display(Name = "MetaTagCanonical")]
        [MaxLengthString(150)]
        public string MetaTagCanonical { get; set; }

        [Display(Name = "MetaTagDescreption")]
        [RequiredForDraft(nameof(IsDraft))]
        [MaxLengthString(300)]
        public string MetaTagDescreption { get; set; }

        [Display(Name = "ProductDescription")]
        [RequiredForDraft(nameof(IsDraft))]
        public string Description { get; set; }

        [Display(Name = "IsDraft")]
        public bool IsDraft { get; set; }

        [Display(Name = "ProductDescription")]
        [RequiredForDraft(nameof(IsDraft))]
        [GUID]
        public string ProductImagesId { get; set; }

        [Display(Name = "Properties")]
        public List<InpAddProduct_Properties> Properties { get; set; }

        [Display(Name = "Keywords")]
        public List<InpAddProduct_Keywords> Keywords { get; set; }

        [Display(Name = "ProductVariants")]
        public List<InpAddProduct_Variants> Variants { get; set; }

        [Display(Name = "PostingRestrictions")]
        public List<InpAddProduct_PostingRestrictions> PostingRestrictions { get; set; }
    }

    public class InpAddProduct_Properties
    {

        [Display(Name = "PropertyId")]
        [GUID]
        [RequiredString]
        public string Id { get; set; }

        [Display(Name = "Value")]
        [RequiredForDraft("IsDraft")]
        [MaxLengthString(3000)]
        public string Value { get; set; }
    }

    public class InpAddProduct_Keywords
    {
        [Display(Name = "KeywordTitle")]
        [RequiredIfNotDeleted(nameof(IsDelete))]
        [MaxLengthString(250)]
        public string Title { get; set; }

        [Display(Name = "KeywordSimilarityTitle")]
        [RequiredIfNotDeleted(nameof(IsDelete))]
        [MaxLengthString(10)]
        public string Similarity { get; set; }

        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }
    }

    public class InpAddProduct_Variants
    {

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

    public class InpAddProduct_PostingRestrictions
    {

        [Display(Name = "CountryId")]
        [RequiredIfNotDeleted(nameof(IsDelete))]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name = "PostingStatus")]
        public bool Posting { get; set; }

        [Display(Name = "IsDelete")]
        public bool IsDelete { get; set; }
    }
}
