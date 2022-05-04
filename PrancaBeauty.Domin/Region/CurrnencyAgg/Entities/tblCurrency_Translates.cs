using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.CurrnencyAgg.Entities
{
    public class tblCurrency_Translates : IEntity
    {
        public Guid Id { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblCurrencies tblCurrency { get; set; }
        public virtual tblLanguages tblLanguage { get; set; }
    }
}
