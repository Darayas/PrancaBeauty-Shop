﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Categories
{
    public class InpAddCategory
    {
        public string ParentId { get; set; }

        public IFormFile Image { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }

        public List<InpAddCategory_Translate> LstTranslate { get; set; }
    }

    public class InpAddCategory_Translate
    {
        public string LangId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}

