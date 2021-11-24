using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Products
{
    public class InpGetProductsForManage
    {
        [Display(Name = "Page")]
        public int Page { get; set; }

        [Display(Name = "Take")]
        public int Take { get; set; }

        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "SellerUserId")]
        [GUID]
        public string SellerUserId { get; set; }

        [Display(Name = "AuthorUserId")]
        [GUID]
        public string AuthorUserId { get; set; }

        [Display(Name = "Title")]
        [GUID]
        public string Title { get; set; }

        [Display(Name = "Name")]
        [MaxLength(150, ErrorMessage = "MaxLengthMsg")]
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
