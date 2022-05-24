using Framework.Domain.Contracts;
using System;

namespace PrancaBeauty.Domin.Keywords.SearchHistoryAgg.Entities
{
    public class tblSearchHistory : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int CountSearch { get; set; }
    }
}
