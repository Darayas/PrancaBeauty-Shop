using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductPropertiesValues
{
    public class InpRemovePropertiesByProductId
    {
        [Display(Name = "ProductId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string ProductId { get; set; }
    }
}
