﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viPaymentVeryfication
    {
        [Display(Name = "BillNumber")]
        [RequiredString]
        [MaxLengthString(100)]
        public string BillNumber { get; set; }
    }
}
