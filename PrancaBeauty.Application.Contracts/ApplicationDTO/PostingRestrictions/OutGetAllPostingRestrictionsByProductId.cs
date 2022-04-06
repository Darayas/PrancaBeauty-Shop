using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.PostingRestrictions
{
    public class OutGetAllPostingRestrictionsByProductId
    {
        public string Id { get; set; }
        public string CountryId { get; set; }
        public bool Posting { get; set; }
    }
}
