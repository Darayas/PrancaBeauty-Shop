using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viGetEditSlide
    {
        public string ReturnUrl { get; set; }

        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }
    }
}
