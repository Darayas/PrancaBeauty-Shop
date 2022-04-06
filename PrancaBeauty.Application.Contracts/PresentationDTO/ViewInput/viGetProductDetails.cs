using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
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
