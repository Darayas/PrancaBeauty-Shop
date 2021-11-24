using Framework.Common.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PostingRestrictions
{
    public class InpAddPostingRestrictionsToProduct
    {
        [Display(Name = "ProductId")]
        [Required(ErrorMessage = "Required")]
        [GUID]
        public string ProductId { get; set; }
        public List<InpAddPostingRestrictionsToProduct_Restrictions> PostingRestrictions { get; set; }
    }

    public class InpAddPostingRestrictionsToProduct_Restrictions
    {
        [Display(Name = "CountryId")]
        [GUID]
        [Required(ErrorMessage = "Required")]
        public string CountryId { get; set; }

        [Display(Name = "PostingStatus")]
        public bool Posting { get; set; }
    }
}
