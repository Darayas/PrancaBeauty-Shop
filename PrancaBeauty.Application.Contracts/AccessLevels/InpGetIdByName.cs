using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class InpGetIdByName
    {
        [Display(Name = "Name")]
        [RequiredString]
        [GUID]
        public string Name { get; set; }
    }
}
