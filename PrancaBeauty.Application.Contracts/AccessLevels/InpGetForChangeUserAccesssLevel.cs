using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class InpGetForChangeUserAccesssLevel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(150, ErrorMessage = "MaxLengthMsg")]
        public string Name { get; set; }
    }
}
