using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompo_Combo_Categories
    {
        public string id { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public bool hasChildren { get; set; }
    }
}
