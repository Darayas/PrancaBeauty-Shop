using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductPropertiesValues
{
    public class InpAddPropertiesToProduct
    {
        [Display(Name = "ProductId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string ProductId { get; set; }

        public List<InpAddPropertiesToProduct_Items> PropItems { get; set; }
    }

    public class InpAddPropertiesToProduct_Items
    {
        [Display(Name = "ProductId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100,ErrorMessage = "MaxLengthMsg")]
        public string Id { get; set; }

        [Display(Name = "Value")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string Value { get; set; }
    }
}
