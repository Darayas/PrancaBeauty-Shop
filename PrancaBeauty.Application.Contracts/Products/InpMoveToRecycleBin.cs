﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Products
{
    public class InpMoveToRecycleBin
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string AuthorUserId { get; set; }
    }
}
