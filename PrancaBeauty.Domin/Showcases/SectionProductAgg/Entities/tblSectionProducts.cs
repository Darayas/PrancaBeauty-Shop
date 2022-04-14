using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Showcases.SectionProductAgg.Entities
{
    public class tblSectionProducts : IEntity
    {
        public Guid Id { get; set; }
        public Guid ShowcaseTabSectionId { get; set; }
        public Guid ProductId { get; set; }
        public int Sort { get; set; }

        public virtual tblShowcaseTabSections tblShowcaseTabSections { get; set; }
        public virtual tblProducts tblProducts { get; set; }
    }
}
