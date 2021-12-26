using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.KeywordProducts
{
    public class InpEditProductKeywords
    {
        [Display(Name = "ProductId")]
        [RequiredString]
        [GUID]
        public string ProductId { get; set; }
        public List<InpEditProductKeywords_LstKeywords> LstKeywords { get; set; }
    }

    public class InpEditProductKeywords_LstKeywords
    {
        [Display(Name = "KeywordTitle")]
        [RequiredString]
        [MaxLengthString(250)]
        public string Title { get; set; }

        [Display(Name = "KeywordSimilarityTitle")]
        [RequiredString]
        [MaxLengthString(10)]
        public string Similarity { get; set; }
    }
}
