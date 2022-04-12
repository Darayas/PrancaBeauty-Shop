using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Sliders
{
    public class OutGetLstSlidesForMainSlider
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ImgUrl { get; set; }
        public bool IsFollow { get; set; }
    }
}
