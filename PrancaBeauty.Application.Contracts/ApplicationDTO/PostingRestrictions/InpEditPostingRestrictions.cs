using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.PostingRestrictions
{

    public class InpEditPostingRestrictions
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
        public List<InpEditPostingRestrictions_Restrictions> PostingRestrictions { get; set; }
    }

    public class InpEditPostingRestrictions_Restrictions
    {
        [Display(Name = "CountryId")]
        [GUID]
        [RequiredString]
        public string CountryId { get; set; }

        [Display(Name = "PostingStatus")]
        public bool Posting { get; set; }
    }
}
