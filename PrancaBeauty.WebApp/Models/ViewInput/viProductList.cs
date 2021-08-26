using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viProductList
    {
        public string LangId { get; set; }
        public string SellerUserId { get; set; }
        public string AuthorUserId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsDraft { get; set; }
        public bool? IsConfirmed { get; set; }
        public bool? IsSchedule { get; set; }
    }
}
