﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Bills
{
    public class InpStartPayment
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "CurrencyId")]
        [RequiredString]
        [GUID]
        public string CurrencyId { get; set; }

        [Display(Name = "BillNumber")]
        [RequiredString]
        [MaxLengthString(100)]
        public string BillNumber { get; set; }

        [Display(Name = "CallBackUrl")]
        [RequiredString]
        [MaxLengthString(250)]
        public string CallBackUrl { get; set; }
    }
}
