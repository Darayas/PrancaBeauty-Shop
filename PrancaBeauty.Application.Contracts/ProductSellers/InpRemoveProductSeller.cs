﻿using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductSellers
{
    public class InpRemoveProductSeller
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "ProductSellerId")]
        [RequiredString]
        [GUID]
        public string ProductSellerId { get; set; }
    }
}
