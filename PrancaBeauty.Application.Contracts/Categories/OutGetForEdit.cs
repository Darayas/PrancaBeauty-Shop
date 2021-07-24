using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Categories
{
    public class OutGetForEdit
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string ImgCategoryUrl { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public List<OutGetForEdit_Translate> LstTranslate { get; set; }
    }

    public class OutGetForEdit_Translate
    {
        public string LangId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
