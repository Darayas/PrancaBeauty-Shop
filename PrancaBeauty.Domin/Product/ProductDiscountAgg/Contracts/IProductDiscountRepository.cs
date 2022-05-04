﻿using Framework.Domain.Contracts;
using PrancaBeauty.Domin.Product.ProductDiscountAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductDiscountAgg.Contracts
{
    public interface IProductDiscountRepository : IRepository<tblProductDiscounts>
    {
    }
}
