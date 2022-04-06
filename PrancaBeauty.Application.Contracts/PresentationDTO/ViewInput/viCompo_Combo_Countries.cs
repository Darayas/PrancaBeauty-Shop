using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_Combo_Countries
    {
        public string CountryId { get; set; }
        public string FieldName { get; set; }
        public bool ShowLabel { get; set; }
    }
}
