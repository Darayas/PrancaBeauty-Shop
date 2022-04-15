using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmListShowcases
    {
        public string Id { get; set; }

        [Display(Name = "CountyTitle")]
        public string CountyTitle { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "IsFullWidth")]
        public bool IsFullWidth { get; set; }

        [Display(Name = "Sort")]
        public int Sort { get; set; }

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "StartDate")]
        public string StartDate { get; set; }

        [Display(Name = "EndDate")]
        public string EndDate { get; set; }
    }
}
