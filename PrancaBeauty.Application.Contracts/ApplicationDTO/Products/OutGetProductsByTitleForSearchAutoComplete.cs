﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Products
{
    public class OutGetProductsByTitleForSearchAutoComplete
    {
        public string Id { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
}
