using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viAddProductSeller
    {
        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }
    }
}
