using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ProductPropertiesValues
{
    public class InpAddPropertiesToProduct
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        public List<InpAddPropertiesToProduct_Items> PropItems { get; set; }
    }

    public class InpAddPropertiesToProduct_Items
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Id { get; set; }

        [Display(Name = "Value")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Value { get; set; }
    }
}
