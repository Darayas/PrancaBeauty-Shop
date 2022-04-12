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
    public class OutGetSlideForEdit
    {
        public string Id { get; set; }

        public string FileId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public bool IsFollow { get; set; } // Follow, NoFollow

        public bool IsEnable { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
