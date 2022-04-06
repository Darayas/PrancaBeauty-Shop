using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Categories
{
    public class OutGetListForAdminPage
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string ImgUrl { get; set; }
        public string ParentTitle  { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Sort { get; set; }

    }
}
