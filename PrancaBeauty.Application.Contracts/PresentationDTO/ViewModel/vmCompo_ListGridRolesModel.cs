using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompo_ListGridRolesModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }

        [Display(Name= "PageName")]
        public string PageName { get; set; }
        public int Sort { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        public bool HasChild { get; set; }
    }
}
