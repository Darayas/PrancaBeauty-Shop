using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductPropertisAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductPropertisAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductProperties
{
    public class ProductPropertis_TranslatesRepository : BaseRepository<tblProductPropertis_Translates>, IProductPropertis_TranslatesRepository
    {
        public ProductPropertis_TranslatesRepository(MainContext Context) : base(Context)
        {

        }
    }
}
