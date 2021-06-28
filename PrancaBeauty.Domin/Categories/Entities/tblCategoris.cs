using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Categories.Entities
{
    public class tblCategoris : IEntity
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ImageId { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }

        public virtual tblCategoris tblCategory_Parent { get; set; }
        public virtual tblFiles tblFiles { get; set; }
        public virtual ICollection<tblCategoris> tblCategory_Childs { get; set; }
        public virtual ICollection<tblCategory_Translates> tblCategory_Translates { get; set; }
    }
}
