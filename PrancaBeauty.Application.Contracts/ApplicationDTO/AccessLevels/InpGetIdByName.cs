using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.AccessLevels
{
    public class InpGetIdByName
    {
        [Display(Name = "Name")]
        [RequiredString]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
