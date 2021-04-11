using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.AccessLevels
{
    public class OutGetListForAdminPage
    {
        public string Id { get; set; }

        [Display(Name= "Title")]
        public string Name { get; set; }

        [Display(Name = "CountUser")]
        public int CountUser { get; set; }
    }
}
