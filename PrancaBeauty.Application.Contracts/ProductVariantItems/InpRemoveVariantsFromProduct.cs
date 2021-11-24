using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductVariantItems
{
    public class InpRemoveVariantsFromProduct
    {
        [Display(Name = "CountryId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string ProductId { get; set; }

    }
}
