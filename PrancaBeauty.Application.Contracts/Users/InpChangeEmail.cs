using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Users
{
    public class InpChangeEmail
    {
        [Display(Name = "Token")]
        [Required(ErrorMessage = "Required")]
        [MaxLength(1000, ErrorMessage = "MaxLength")]
        public string Token { get; set; }
    }
}
