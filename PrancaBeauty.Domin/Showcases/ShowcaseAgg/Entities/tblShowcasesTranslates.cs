using Framework.Domain;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities
{
    public class tblShowcasesTranslates : IEntity
    {
        public Guid Id { get; set; }
        public Guid LangId { get; set; }
        public Guid ShowcaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual tblLanguages tblLanguage { get; set; }
        public virtual tblShowcases tblShowcase { get; set; }
    }
}
