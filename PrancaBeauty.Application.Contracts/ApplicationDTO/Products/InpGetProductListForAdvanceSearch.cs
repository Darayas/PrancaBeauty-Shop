﻿using Framework.Application.Enums;
using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Products
{
    public class InpGetProductListForAdvanceSearch
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "CategoryName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string CategoryName { get; set; }

        [Display(Name = "KeywordName")]
        [MaxLengthString(100)]
        public string KeywordName { get; set; }

        [Display(Name = "MinPrice")]
        public double MinPrice { get; set; }

        [Display(Name = "MaxPrice")]
        public double MaxPrice { get; set; }

        [Display(Name = "Sort")]
        public GetProductListForAdvanceSearchSortingEnum Sort { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue)]
        public int Take { get; set; }

        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue)]
        public int CurrentPage { get; set; }
    }
}
