using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Roles
{
    public class InpListOfRoles
    {
        [Display(Name = "ParentId")]
        [GUID]
        public string ParentId { get; set; }
    }
}
