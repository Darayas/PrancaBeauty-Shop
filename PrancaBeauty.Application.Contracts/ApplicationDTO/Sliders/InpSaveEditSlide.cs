using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders
{
    public class InpSaveEditSlide
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "FileId")]
        [RequiredString]
        [GUID]
        public string FileId { get; set; }

        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(200)]
        public string Title { get; set; }

        [Display(Name = "Url")]
        [RequiredString]
        [MaxLengthString(500)]
        [ItsForUrl]
        public string Url { get; set; }

        [Display(Name = "IsFollow")]
        public bool IsFollow { get; set; } // Follow, NoFollow

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        [Display(Name = "StartDate")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "EndDate")]
        public DateTime? EndDate { get; set; }
    }
}
