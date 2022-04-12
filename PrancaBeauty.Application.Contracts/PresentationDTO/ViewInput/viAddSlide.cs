using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viAddSlide
    {
        [Display(Name ="Title")]
        [RequiredString]
        [MaxLengthString(200)]
        public string Title { get; set; }

        [Display(Name = "Url")]
        [RequiredString]
        [MaxLengthString(500)]
        [ItsForUrl]
        public string Url { get; set; }
        public bool IsFollow { get; set; } // Follow, NoFollow
        public bool IsEnable { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "StartDate")]
        [Date]
        public string StartDate { get; set; }

        [Display(Name = "EndDate")]
        [MaxLengthString(150)]
        [Date]
        public string EndDate { get; set; }
    }
}
