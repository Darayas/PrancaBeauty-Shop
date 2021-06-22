using Framework.Domain;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Categories.Entities
{
    public class tblCategory_Translates : IEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblCategoris tblCategoris { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
    }
}
