using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.KeywordProducts
{
    public class OutGetKeywordByProductId
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double Similarity { get; set; }
    }
}
