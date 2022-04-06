using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductPropertiesValues
{

    public class InpEditProductProperties
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        public List<InpEditProductProperties_Items> PropItems { get; set; }
    }

    public class InpEditProductProperties_Items
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
