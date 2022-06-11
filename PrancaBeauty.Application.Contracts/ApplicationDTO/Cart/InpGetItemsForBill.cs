using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Cart
{
    public class InpGetItemsForBill
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }
    }
}
