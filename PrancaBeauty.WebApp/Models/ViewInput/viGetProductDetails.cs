using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viGetProductDetails
    {
        [Display(Name="ProductName")]
        [RequiredString]
        [MaxLengthString(250)]
        [ItsForUrl]
        public string Name { get; set; }
    }
}
