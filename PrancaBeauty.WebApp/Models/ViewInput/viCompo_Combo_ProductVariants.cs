using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_Combo_ProductVariants
    {
        public string FieldName { get; set; }
        public string VariantId { get; set; }
        public bool IsEnable { get; set; } = true;
    }
}
