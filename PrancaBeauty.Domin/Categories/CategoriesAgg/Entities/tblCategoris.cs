using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionFreeItemAgg.Entities;
using PrancaBeauty.Domin.Showcases.SectionProductCategoryAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Categories.CategoriesAgg.Entities
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
        public virtual ICollection<tblProducts> tblProducts { get; set; }
        public virtual ICollection<tblSectionProductCategory> tblSectionProductCategory { get; set; }

    }
}
