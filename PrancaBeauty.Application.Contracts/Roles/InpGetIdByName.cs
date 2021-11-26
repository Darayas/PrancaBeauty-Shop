using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Roles
{
    public class InpGetIdByName
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(150, ErrorMessage = "MaxLengthMsg")]
        public string Name { get; set; }
    }
}
