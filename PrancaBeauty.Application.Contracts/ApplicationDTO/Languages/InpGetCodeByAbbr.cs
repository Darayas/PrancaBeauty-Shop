using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Languages
{
    public class InpGetCodeByAbbr
    {
        [Display(Name = "Abbr")]
        //[RequiredString]
        //[MaxLengthString(50)]
        public string Abbr { get; set; }
    }
}
