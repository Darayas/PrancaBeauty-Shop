using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Keywords.SearchHistoryAgg.Entities
{
    public class tblSearchHistory : IEntity
    {
        public Guid Id { get; set; }
        public string LangId { get; set; }
        public string Title { get; set; }
        public int CountSearch { get; set; }

        public virtual tblLanguages tblLanguages { get; set; }
    }
}
