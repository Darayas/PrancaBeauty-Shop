using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PostingRestrictions
{
    public class InpAddPostingRestrictionsToProduct
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
        public List<InpAddPostingRestrictionsToProduct_Restrictions> PostingRestrictions { get; set; }
    }

    public class InpAddPostingRestrictionsToProduct_Restrictions
    {
        [Display(Name = "CountryId")]
        [GUID]
        [RequiredString]
        public string CountryId { get; set; }

        [Display(Name = "PostingStatus")]
        public bool Posting { get; set; }
    }
}
