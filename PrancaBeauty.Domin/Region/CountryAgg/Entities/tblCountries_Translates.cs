using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.CountryAgg.Entities
{
    public class tblCountries_Translates : IEntity
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblCountries tblCountries { get; set; }
    }
}
