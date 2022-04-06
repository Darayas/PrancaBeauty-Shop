using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_PostingRestrictionsInput
    {
        public int Index { get; set; }
        public string Id { get; set; }

        [Display(Name = "CountryId")]
        public string CountryId { get; set; }

        [Display(Name = "Posting")]
        public bool Posting { get; set; }

        public bool IsDelete { get; set; }
    }
}
