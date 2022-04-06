using Framework.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductVariantItems
{
    public class OutGetAllVariantsByProductId
    {
        public string Id { get; set; }
        public string VariantId { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public double Percent { get; set; }
        public string GuaranteeId { get; set; }
        public string GuaranteeTitle { get; set; }
        public string ProductCode { get; set; }
        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده
        public bool IsEnable { get; set; }
        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده
        public int CountInStock { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsMain { get; set; }
    }
}
