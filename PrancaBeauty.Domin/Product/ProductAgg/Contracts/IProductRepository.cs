﻿using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductAgg.Contracts
{
    public interface IProductRepository : IRepository<tblProducts>
    {

    }
}
