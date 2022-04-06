using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Users
{
    public class InpGetListForCombo
    {

        [Display(Name = "LangId")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Name")]
        [MaxLengthString(100)]
        public string Name { get; set; }
    }
}
