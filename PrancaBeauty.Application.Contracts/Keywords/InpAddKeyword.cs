using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Keywords
{
    public class InpAddKeyword
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100,ErrorMessage = "MaxLengthMsg")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [MaxLength(5000, ErrorMessage = "MaxLengthMsg")]
        public string Description { get; set; }
    }
}
