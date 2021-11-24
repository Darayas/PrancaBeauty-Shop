﻿using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.AccesslevelRoles
{
    public class InpAddRolesToAccessLevel
    {
        [Display(Name = "AccessLevelId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string AccessLevelId { get; set; }

        [Display(Name = "RolesName")]
        public string[] RolesName { get; set; }
    }
}
