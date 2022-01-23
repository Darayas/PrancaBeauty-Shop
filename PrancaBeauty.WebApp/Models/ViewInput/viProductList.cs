using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viProductList
    {
        [Display(Name = "LangId")]
        public string LangId { get; set; }

        [Display(Name = "SellerId")]
        public string SellerId { get; set; }

        [Display(Name = "AuthorUserId")]
        public string AuthorUserId { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "IsDelete")]
        public bool? IsDelete { get; set; }

        [Display(Name = "IsDraft")]
        public bool? IsDraft { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool? IsConfirmed { get; set; }

        [Display(Name = "IsSchedule")]
        public bool? IsSchedule { get; set; }
    }
}
