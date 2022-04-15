using Framework.Domain;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Entities;
using PrancaBeauty.Domin.Showcases.SectionProductKeywordAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Keywords.KeywordAgg.Entities
{
    public class tblKeywords : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<tblKeywords_Products> tblKeywords_Products { get; set; }
        public virtual ICollection<tblSectionProductKeyword> tblSectionProductKeyword { get; set; }
    }
}
