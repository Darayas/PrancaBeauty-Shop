﻿using PrancaBeauty.WebApp.Common.DataAnnotations;
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
        public string ReturnUrl { get; set; }

        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID(ErrorMessage = "GUIDMsg")]
        public string LangId { get; set; }

        [Display(Name = "TopicId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID(ErrorMessage = "GUIDMsg")]
        public string TopicId { get; set; }

        [Display(Name = "CategoryId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID(ErrorMessage = "GUIDMsg")]
        public string CategoryId { get; set; }

        [Display(Name = "ProductName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(250,MinimumLength =3,ErrorMessage = "StringLengthMsg")]
        public string Name { get; set; } // Uniqe Name

        [Display(Name = "ProductTitle")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "StringLengthMsg")]
        public string Title { get; set; }

        [Display(Name = "ProductDate")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string Date { get; set; }

        [Display(Name = "ProductPrice")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string Price { get; set; }

        [Display(Name = "MetaTagKeyword")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string MetaTagKeyword { get; set; }

        [Display(Name = "MetaTagCanonical")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string MetaTagCanonical { get; set; }

        [Display(Name = "MetaTagDescreption")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string MetaTagDescreption { get; set; }

        [Display(Name = "ProductDescription")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string Description { get; set; }

        public List<viAddProduct_Properties> Properties { get; set; }
        public List<viAddProduct_Keywords> Keywords { get; set; }
        public List<viAddProduct_Variants> Variants { get; set; }
        public List<viAddProduct_PostingRestrictions> PostingRestrictions { get; set; }
    }

    public class viAddProduct_Properties
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }

    public class viAddProduct_Keywords
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Similarity { get; set; }
    }

    public class viAddProduct_Variants
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public string Percent { get; set; }
        public string GuaranteeId { get; set; }
    }
    
    public class viAddProduct_PostingRestrictions
    {
        public string Id { get; set; }
        public string CountryId { get; set; }
        public string Posting { get; set; }
    }
}
