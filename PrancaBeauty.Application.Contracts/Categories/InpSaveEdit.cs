using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Categories
{
    public class InpSaveEdit
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ParentId { get; set; }
        public IFormFile Image { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public List<InpSaveEdit_Translate> LstTranslate { get; set; }
    }

    public class InpSaveEdit_Translate
    {
        public string LangId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

