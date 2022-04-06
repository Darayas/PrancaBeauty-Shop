using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductMedia
{
    public class InpAddMediasToProduct
    {
        [Display(Name ="ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }

        [Display(Name = "MediaIds")]
        [RequiredString]
        [GUID]
        public string MediaIds { get; set; }

    }
}
