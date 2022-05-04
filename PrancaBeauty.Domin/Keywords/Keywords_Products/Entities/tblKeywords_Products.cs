using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Keywords.KeywordAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Keywords.Keywords_Products.Entities
{
    public class tblKeywords_Products : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid KeywordId { get; set; }
        /// <summary>
        /// درصد تشابه
        /// </summary>
        public double Similarity { get; set; }

        public virtual tblKeywords tblKeywords { get; set; }
        public virtual tblProducts tblProducts { get; set; }

    }
}
