using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Showcases.SectionCategoryAgg.Entities
{
    public class tblSectionCategoryTranslate : IEntity
    {
        public Guid Id { get; set; }
        public Guid LangId { get; set; }
        public Guid SectionCategoryId { get; set; }
        public Guid FileId { get; set; }
        public string Title { get; set; }

        public virtual tblFiles tblFiles { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
        public virtual tblSectionCategories tblSectionCategories { get; set; }
    }
}
