using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Cart
{
    public class InpChangeQtyCart
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        public List<InpChangeQtyCartItem> Items { get; set; } = new List<InpChangeQtyCartItem>();
    }

    public class InpChangeQtyCartItem
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "Qty")]
        [RequiredString]
        [NumRange(1, 100)]
        public int Qty { get; set; }
    }
}
