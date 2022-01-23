using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductSellers
{
    public class InpAddSellerToProdcut
    {
        [Display(Name = "SellerId")]
        [RequiredString]
        [GUID]
        public string SellerId { get; set; }

        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "IsConfirm")]
        public bool IsConfirm { get; set; }
    }
}
