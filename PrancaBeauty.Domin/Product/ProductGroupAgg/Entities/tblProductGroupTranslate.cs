using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Product.ProductGroupAgg.Entities
{
    public class tblProductGroupTranslate : IEntity
    {
        public Guid Id { get; set; }
        public Guid LangId { get; set; }
        public Guid ProductGroupId { get; set; }
        public string Title { get; set; }

        public virtual tblLanguages tblLanguages { get; set; }
        public virtual tblProductGroups tblProductGroups { get; set; }
    }
}
