﻿using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Products
{
    public class InpGetProductsByTitle
    {
        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue)]
        public int Page { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue)]
        public int Take { get; set; }

    }
}
