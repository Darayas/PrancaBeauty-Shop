using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductPrice
{
    public class InpAddPriceToProduct
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "CurrencyId")]
        [RequiredString]
        [GUID]
        public string CurrencyId { get; set; }

        [Display(Name = "Price")]
        [RequiredString]
        public string Price { get; set; }
    }
}
