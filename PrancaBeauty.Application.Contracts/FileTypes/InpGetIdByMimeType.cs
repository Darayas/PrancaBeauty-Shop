using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.FileTypes
{
    public class InpGetIdByMimeType
    {
        [Display(Name = "MimeType")]
        [RequiredString]
        [MaxLengthString(100)]
        public string MimeType { get; set; }
    }
}
