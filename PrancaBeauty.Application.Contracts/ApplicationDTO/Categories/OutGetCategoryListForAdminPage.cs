using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Categories
{
    public class OutGetCategoryListForAdminPage
{
        public string Id { get; set; }
        public string TopicTitle { get; set; }
        public string ParentId { get; set; }
        public string ImgUrl { get; set; }
        public string ParentTitle  { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Sort { get; set; }

    }
}
