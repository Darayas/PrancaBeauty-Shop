using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Categories
{
    public class OutGetListForCombo
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public bool hasChildren { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public int Sort { get; set; }
    }
}
