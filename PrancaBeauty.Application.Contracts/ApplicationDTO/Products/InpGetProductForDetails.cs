using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Products
{
    public class InpGetProductForDetails
    {
        public bool CheckConfirm { get; set; } = true;

        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "ProductName")]
        [RequiredString]
        [MaxLengthString(250)]
        [ItsForUrl]
        public string Name { get; set; }
    }
}
