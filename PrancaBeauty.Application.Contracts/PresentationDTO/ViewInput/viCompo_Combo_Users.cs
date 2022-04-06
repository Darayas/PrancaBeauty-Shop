using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_Combo_Users
    {
        public string UserId { get; set; }
        public string LangId { get; set; }
        public string FieldName { get; set; }
        public bool ShowLabale { get; set; }
        public bool IsEnable { get; set; } = true;
    }
}
