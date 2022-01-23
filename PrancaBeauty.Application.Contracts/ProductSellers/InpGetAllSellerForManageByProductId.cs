using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductSellers
{
    public class InpGetAllSellerForManageByProductId
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "FullName")]
        [MaxLengthString(100)]
        public string FullName { get; set; }

        [Display(Name = "Page")]
        [Range(1,int.MaxValue,ErrorMessage = "NumRangeMsg")]
        public int Page { get; set; }

        [Display(Name = "Page")]
        [Range(1, int.MaxValue, ErrorMessage = "NumRangeMsg")]
        public int  Take { get; set; }
    }
}
