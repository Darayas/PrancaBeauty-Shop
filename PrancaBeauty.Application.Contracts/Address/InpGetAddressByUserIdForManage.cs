using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Address
{
    public class InpGetAddressByUserIdForManage
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Search")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(150,ErrorMessage = "MaxLengthMsg")]
        public string Search { get; set; }

        [Display(Name = "Search")]
        [Range(1,int.MaxValue,ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int PageNum { get; set; }

        [Display(Name = "Search")]
        [Range(1, int.MaxValue, ErrorMessage = "RangeGreaterThanZeroMsgMsg")]
        public int Take { get; set; }
    }
}
