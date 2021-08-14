using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductPropertiesValues
{
    public class ProductPropertiesValuesRepository : BaseRepository<tblProductPropertiesValues>, IProductPropertiesValuesRepository
    {
        public ProductPropertiesValuesRepository(MainContext Context) : base(Context)
        {

        }
    }
}
