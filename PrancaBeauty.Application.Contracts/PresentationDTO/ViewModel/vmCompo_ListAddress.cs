using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompo_ListAddress
    {
        public string Id { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "CountBills")]
        public int CountBills { get; set; }
    }
}
