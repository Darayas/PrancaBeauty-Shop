using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems
{
    public class InpCheckHasPurchaseForVariant
    {
        [Display(Name = "VariantItemId")]
        [RequiredString]
        [GUID]
        public string VariantItemId { get; set; }
    }
}
