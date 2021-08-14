using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductPropertisAgg.Entities
{
    public class tblProductPropertis : IEntity
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }

        public virtual tblProductTopic tblProductTopic { get; set; }
        public virtual ICollection<tblProductPropertis_Translates> tblProductPropertis_Translates { get; set; }
    }
}
