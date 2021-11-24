using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Address
{
    public class InpRemoveAddress
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string UserId { get; set; }
    }
}
