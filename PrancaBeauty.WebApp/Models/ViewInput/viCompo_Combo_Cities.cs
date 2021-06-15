using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_Combo_Cities
    {
        [Required]
        public string ProvinceId { get; set; }
        public string CityId { get; set; }
        public string FieldName { get; set; }
    }
}
