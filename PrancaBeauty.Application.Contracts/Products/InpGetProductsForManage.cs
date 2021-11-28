using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.Products
{
    public class InpGetProductsForManage
    {
        [Display(Name = "Page")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Page { get; set; }

        [Display(Name = "Take")]
        [NumRange(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Take { get; set; }

        [Display(Name = "LangId")]
        [RequiredString]
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
        [MaxLengthString(150)]
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
