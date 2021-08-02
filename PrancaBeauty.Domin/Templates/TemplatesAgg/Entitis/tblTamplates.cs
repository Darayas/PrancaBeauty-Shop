using Framework.Domain;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Templates.TemplatesAgg.Entitis
{
    public class tblTamplates : IEntity
    {
        public Guid Id { get; set; }
        public Guid LangId { get; set; }
        public string Name { get; set; }
        public string Template { get; set; }

        public virtual tblLanguages tblLanguages { get; set; }
    }
}
