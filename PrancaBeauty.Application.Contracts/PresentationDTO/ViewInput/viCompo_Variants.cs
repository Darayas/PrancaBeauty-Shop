using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_Variants
    {
        public string FieldName { get; set; }
        public string ProductId { get; set; }
        public string SellerId { get; set; }
        public string VariantId { get; set; }
        public bool ProductVariantEnable { get; set; } = true;
    }
}
