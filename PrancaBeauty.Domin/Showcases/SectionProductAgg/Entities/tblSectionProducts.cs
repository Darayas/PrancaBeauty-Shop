using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionItems.Entitiy;
using PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Showcases.SectionProductAgg.Entities
{
    public class tblSectionProducts : IEntity
    {
        public Guid Id { get; set; }
        public Guid TabSectionItemId { get; set; }
        public Guid ProductId { get; set; }

        public virtual tblShowcaseTabSectionItems tblShowcaseTabSectionItems { get; set; }
        public virtual tblProducts tblProducts { get; set; }
    }
}
