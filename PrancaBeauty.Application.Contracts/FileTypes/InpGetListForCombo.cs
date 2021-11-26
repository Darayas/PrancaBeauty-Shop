using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.FileTypes
{
    public class InpGetListForCombo
    {
        [Display(Name = "Title")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string Title { get; set; }
    }
}
