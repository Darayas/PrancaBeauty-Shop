using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.AccessLevels
{
    public class InpUpdateAccessLevel
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(150)]
        public string Name { get; set; }

        [Display(Name = "Roles")]
        public string[] Roles { get; set; }
    }
}
