using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders
{
    public class OutGetListSlideForManage
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ImgUrl { get; set; }

        public string Url { get; set; }

        public int Sort { get; set; }

        public bool IsEnable { get; set; }

        public bool IsActive { get; set; }
        public DateTime Date { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}

