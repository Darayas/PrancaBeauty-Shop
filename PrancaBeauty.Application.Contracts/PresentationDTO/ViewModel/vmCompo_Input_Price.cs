using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompo_Input_Price
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Symbol { get; set; }
        public string vMax { get; set; } // حداکثر تعداد ارقام
        public string mDec { get; set; } // حداکثر تعداد ارقام اعشار
        public string aDec { get; set; } // کاراکتر اعشار
        public string aSep { get; set; } // کاراکتر جداکننده ی بین سه رقم
    }
}
