using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductAsks
{
    public class InpChanageStatusAsk
{
        [Display(Name = "AuthorUserId")]
        [GUID]
        public string AuthorUserId { get; set; }

        [Display(Name = "AskId")]
        [RequiredString]
        [GUID]
        public string AskId { get; set; }
    }
}
