using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class InpAddNewAccessLevel
    {
        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(150)]
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }
}
