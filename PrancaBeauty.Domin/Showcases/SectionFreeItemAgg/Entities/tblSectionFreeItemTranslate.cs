using Framework.Domain.Contracts;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities
{
    public class tblSectionFreeItemTranslate : IEntity
    {
        public Guid Id { get; set; }
        public Guid LangId { get; set; }
        public Guid SectionFreeItemId { get; set; }
        public Guid FileId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string HtmlText { get; set; }

        public virtual tblFiles tblFiles { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
        public virtual tblSectionFreeItems tblSectionFreeItems { get; set; }
    }
}
