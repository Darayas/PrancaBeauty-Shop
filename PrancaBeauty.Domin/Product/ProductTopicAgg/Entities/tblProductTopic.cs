using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Product.ProductPropertisAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductTopicAgg.Entities
{
    public class tblProductTopic : IEntity
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public string Name { get; set; }

        public virtual tblFiles tblFiles { get; set; }
        public virtual ICollection<tblProductTopic_Translates> tblProductTopic_Translates { get; set; }
        public virtual ICollection<tblProductPropertis> tblProductPropertis { get; set; }
    }
}
