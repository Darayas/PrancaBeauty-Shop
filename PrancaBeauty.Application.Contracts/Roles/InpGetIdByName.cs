using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Roles
{
    public class InpGetIdByName
    {
        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(150)]
        public string Name { get; set; }
    }
}
