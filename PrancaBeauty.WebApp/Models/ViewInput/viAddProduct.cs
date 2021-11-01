using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.WebApp.Common.DataAnnotations;
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
        [GUID(ErrorMessage = "GUIDMsg")]
        public string LangId { get; set; }

        [Display(Name = "TopicId")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        [GUID(ErrorMessage = "GUIDMsg")]
        public string TopicId { get; set; }

        [Display(Name = "CategoryId")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        [GUID(ErrorMessage = "GUIDMsg")]
        public string CategoryId { get; set; }

        [Display(Name = "ProductName")]
        [RequiredForDraft(nameof(IsDraft), ErrorMessage = "RequiredMsg")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        public string Name { get; set; } // Uniqe Name

        [Display(Name = "ProductTitle")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
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

        public bool IsDraft { get; set; }

        public List<viAddProduct_Properties> Properties { get; set; }
        public List<viAddProduct_Keywords> Keywords { get; set; }
        public List<viAddProduct_Variants> Variants { get; set; }
        public List<viAddProduct_PostingRestrictions> PostingRestrictions { get; set; }
    }

    public class viAddProduct_Properties
    {
        [Display(Name ="PropertyId")]
        [GUID(ErrorMessage = "GUIDMsg")]
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
        public string Title { get; set; }
        public string Value { get; set; }
        public string Percent { get; set; }
        public string GuaranteeId { get; set; }
        public string ProductCode { get; set; }
        public tblProductVariantItems_SendBy SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده
        public bool IsEnable { get; set; }
        public tblProductVariantItems_SendFrom SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده
        public int CountInStock { get; set; }
    }

    public class viAddProduct_PostingRestrictions
    {
        public string Id { get; set; }

        [Display(Name = "CountryId")]
        [GUID(ErrorMessage = "GUIDMsg")]
        [Required(ErrorMessage = "Required")]
        public string CountryId { get; set; }

        [Display(Name = "PostingStatus")]
        public bool Posting { get; set; }
    }
}
