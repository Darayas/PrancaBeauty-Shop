using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viGetListSellers
    {
        [Display]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
    }
}
