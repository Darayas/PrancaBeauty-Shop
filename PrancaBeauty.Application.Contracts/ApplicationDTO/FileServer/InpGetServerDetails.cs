using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.FileServer
{
    public class InpGetServerDetails
    {
        [Display(Name = "ServerName")]
        [RequiredString]
        [MaxLengthString(100)]
        public string ServerName { get; set; }
    }
}
