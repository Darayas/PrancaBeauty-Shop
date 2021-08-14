using Framework.Domain;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductTopicAgg.Entities
{
    public class tblProductTopic_Translates : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductTopicId { get; set; }
        public Guid LangId { get; set; }
        public string Title { get; set; }

        public virtual tblProductTopic tblProductTopic { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
    }
}
