﻿using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Products
{
    public class InpGetTitleById
    {
        [Display]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
    }
}
