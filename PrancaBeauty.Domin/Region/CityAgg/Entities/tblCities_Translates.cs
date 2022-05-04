using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.CityAgg.Entities
{
    public class tblCities_Translates : IEntity
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblCities tblCity { get; set; }
        public virtual tblLanguages tblLanguage { get; set; }
    }
}
