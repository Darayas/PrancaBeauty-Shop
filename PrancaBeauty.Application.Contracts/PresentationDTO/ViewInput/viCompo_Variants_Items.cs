using Framework.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_Variants_Items
    {
        public int Index { get; set; }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public string Percent { get; set; }
        public string GuaranteeId { get; set; }
        public string ProductCode { get; set; }
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده
        public bool IsEnable { get; set; }
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده
        public int CountInStock { get; set; }
        public bool IsDelete { get; set; }
        public bool IsMain { get; set; }
    }
}
