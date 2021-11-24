using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class InpGetForEdit
    {
        [Display(Name = "AccessLevelId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string AccessLevelId { get; set; }
    }
}
