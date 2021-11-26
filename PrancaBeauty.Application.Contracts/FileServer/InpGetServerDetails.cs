using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.FileServer
{
    public class InpGetServerDetails
    {
        [Display(Name = "ServerName")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string ServerName { get; set; }
    }
}
