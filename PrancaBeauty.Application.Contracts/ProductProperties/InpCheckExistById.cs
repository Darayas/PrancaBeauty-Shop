using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductProperties
{
    public class InpCheckExistById
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string Id { get; set; }

    }
}
